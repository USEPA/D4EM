Imports atcUtility
Imports MapWinUtility

''' <summary>
''' All of the parameters available for affecting the internal workings of SDM Project Builder Batch
''' The parameters most likely to be selected externally can be written to and read from a text file.
''' Other parameters are mainly useful for internal testing
''' </summary>
''' <remarks></remarks>
Public Class SDMParameters
    Public SetupHSPF As Boolean = False
    Public SetupSWAT As Boolean = False
    Public MinCatchmentKM2 As Double = 1.0 'Minimum catchment size
    Public MinFlowlineKM As Double = 5.0 'Minimum flowline length

    Public Projects As New Generic.List(Of D4EM.Data.Project)

    Public ProjectsPath As String = ""

    ''' <summary>
    ''' True to delete a project folder if one already exists at the specified location, False to rename new project with a suffix so it does not conflict
    ''' </summary>
    Public OverwriteProject As Boolean = True

    Public DesiredProjection As DotSpatial.Projections.ProjectionInfo = D4EM.Data.Globals.AlbersProjection

    Public ClipCatchments As Boolean = True
    Public SelectedKeys As New Generic.List(Of String)
    Public Catchments As New Generic.List(Of String)
    Public CatchmentsMethod As String = CatchmentsMethods(0) 'TODO: should this be an enum?
    Public Shared CatchmentsMethods() As String = {"NHDPlus", "TauDEM5"}

    Public SoilSource As String = SoilSources(0)
    Public Shared SoilSources() As String = {"STATSGO", "SSURGO"}

    'Public PourPointLatitude As Double = GetNaN()
    'Public PourPointLongitude As Double = GetNaN()
    'Public PourPointMaxKm As Double = GetNaN()

    Public NASSYears As New Generic.List(Of String)
    Public NASSStatistics As Boolean = False

    Public BasinsMetConstituents As New Generic.List(Of String)
    Public NCDCconstituents As New Generic.List(Of String)
    Public NLDASconstituents As New Generic.List(Of String)

    Public ElevationGrid As D4EM.Data.LayerSpecification = D4EM.Data.Source.NHDPlus.LayerSpecifications.ElevationGrid
    Public Shared ElevationGridOptions() As D4EM.Data.LayerSpecification =
        {D4EM.Data.Source.NHDPlus.LayerSpecifications.ElevationGrid,
         D4EM.Data.Source.USGS_Seamless.LayerSpecifications.NED.OneArcSecond,
         D4EM.Data.Source.USGS_Seamless.LayerSpecifications.NED.OneThirdArcSecond} 'TODO: custom/user-defined

    Public KeepConnectingRemovedFlowLines As Boolean = True

    Public SWATDatabaseName As String = atcUtility.FindFile("", "SWAT2005.mdb")
    Public BoundariesOutputs As String = ""
    'Private PresetCatchments As String = "" '"G:\Project\APES-Kraemer\ms_30m_01\Watershed\Shapes"
    'Private MultiThread As Boolean = False
    Public UseMgtCropFile As Boolean = False
    Public ParameterShapefileName As String = ""
    Public CreateArcSWATFiles As Boolean = False

    ''for debugging catchment/flowline aggregation
    'Public ShowCatchmentsFlowlines As Boolean = False 'True to save snapshots showing the simplified catchments and flowlines instead of continuing processing
    'Public MapWindowIndex As Integer = 0
    'Private BufferMeters As Double = 100

    'Simplification parameters: dissolve some HRUs and spread their area among other HRUs in the subbasin
    Public AreaIgnoreBelowFraction As Double = 0       'HRUs smaller than this fraction of their subbasin will be dissolved 
    Public AreaIgnoreBelowAbsolute As Double = 0       'HRUs smaller than this will be dissolved
    Public LandUseIgnoreBelowFraction As Double = 0.07 'Land uses covering less than this much of a subbasin will have all their HRUs in that subbasin dissolved
    Public LandUseIgnoreBelowAbsolute As Double = 0    'Absolute total area of a land use within a subbasin below which to dissolve its HRUs
    Public SimulationStartYear As Integer = 1990
    Public SimulationEndYear As Integer = 2000

    Public HspfOutputInterval As D4EM.Model.HSPF.HspfOutputInterval = D4EM.Model.HSPF.HspfOutputInterval.Hourly
    Public HspfSnowOption As Integer = 0 '0 = default (no snow), 1 = Energy Balance, 2 = Temperature Index (Degree Day)
    Public HspfBacterialOption As Boolean = False 'True to include bacteria in HSPF
    Public HspfChemicalOption As Boolean = False 'True to include land-applied chemical in HSPF
    Public HspfChemicalName As String = "Chemical 1"
    Public HspfChemicalMaximumSolubility As Double = 25.0
    Public HspfChemicalPartitionCoeff() As Double = {4, 4, 2, 2}
    Public HspfChemicalFreundlichExp() As Double = {1.4, 1.4, 1.4, 1.4}
    Public HspfChemicalDegradationRate() As Double = {0.12, 0.045, 0.04, 0.04}

    Public GeoProcess As Boolean = True
    Public BuildDatabase As Boolean = True
    Public RunModel As Boolean = False
    Public OutputSummarize As Boolean = False
    Public ResumeOverlay As Boolean = False  'True to use existing full or partially complete overlay, False to do overlay from the start
    'Water Quality Constituents to set up for HSPF
    Public WQConstituents As New Generic.List(Of String) '() As String = {} '{"NH3+NH4", "NO3", "ORTHO P", "BOD", "SEDIMENT"}

    Public Sub New()
    End Sub

    Public Sub New(ByVal aFilename As String)
        Me.XML = IO.File.ReadAllText(aFilename)
    End Sub

    ''' <summary>
    ''' If this set of SDMParameters only contains one Project, use this property, otherwise use Projects
    ''' </summary>
    ''' <returns>The first or only D4EM.Data.Project in this set of parameters</returns>
    Public Property Project() As D4EM.Data.Project
        Get
            If Projects.Count = 0 Then
                Projects.Add(New D4EM.Data.Project(D4EM.Data.Globals.AlbersProjection, "", "", Nothing, aClip:=True, aMerge:=False))
            End If
            Return Projects(0)
        End Get
        Set(ByVal value As D4EM.Data.Project)
            If Projects.Count = 0 Then
                Projects.Add(value)
            Else
                Projects(0) = value
            End If
        End Set
    End Property

    Public Property XML() As String
        Get
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("<SDMParameters>")
            sb.AppendLine("<ProjectsPath>" & ProjectsPath & "</ProjectsPath>")

            sb.AppendLine("<StreamCatchmentProcessing>")
            sb.AppendLine("    <MinimumStreamLength>" & MinFlowlineKM & "</MinimumStreamLength>")
            sb.AppendLine("    <MinimumCatchmentArea>" & MinCatchmentKM2 & "</MinimumCatchmentArea>")
            sb.AppendLine("    <MinumumLandUsePercent>" & LandUseIgnoreBelowFraction * 100 & "</MinumumLandUsePercent>")
            sb.AppendLine("</StreamCatchmentProcessing>")

            sb.AppendLine("<SimulationStartYear>" & SimulationStartYear & "</SimulationStartYear>")
            sb.AppendLine("<SimulationEndYear>" & SimulationEndYear & "</SimulationEndYear>")

            If SetupSWAT Then
                sb.AppendLine("<SWAT>")
                sb.AppendLine("    <CreateArcSWATFiles>" & CreateArcSWATFiles & "</CreateArcSWATFiles>")
                sb.AppendLine("    <SWAT2005Database>" & SWATDatabaseName & "</SWAT2005Database>")
                'sb.AppendLine("SWATSoilsDatabase," & _swatSoilsDB)
                sb.AppendLine("</SWAT>")
            End If

            If SetupHSPF Then
                sb.AppendLine("<HSPF>")
                sb.AppendLine("    <SnowOption>" & HspfSnowOption & "</SnowOption>")
                sb.AppendLine("    <BacterialOption>" & HspfBacterialOption & "</BacterialOption>")
                sb.AppendLine("    <ChemicalOption>" & HspfChemicalOption & "</ChemicalOption>")
                sb.AppendLine("    <ChemicalName>" & HspfChemicalName & "</ChemicalName>")
                sb.AppendLine("    <ChemicalMaximumSolubility>" & HspfChemicalMaximumSolubility & "</ChemicalMaximumSolubility>")
                sb.AppendLine("    <ChemicalPartitionCoeffSurface>" & HspfChemicalPartitionCoeff(0) & "</ChemicalPartitionCoeffSurface>")
                sb.AppendLine("    <ChemicalPartitionCoeffUpper>" & HspfChemicalPartitionCoeff(1) & "</ChemicalPartitionCoeffUpper>")
                sb.AppendLine("    <ChemicalPartitionCoeffLower>" & HspfChemicalPartitionCoeff(2) & "</ChemicalPartitionCoeffLower>")
                sb.AppendLine("    <ChemicalPartitionCoeffGround>" & HspfChemicalPartitionCoeff(3) & "</ChemicalPartitionCoeffGround>")
                sb.AppendLine("    <ChemicalFreundlichExpSurface>" & HspfChemicalFreundlichExp(0) & "</ChemicalFreundlichExpSurface>")
                sb.AppendLine("    <ChemicalFreundlichExpUpper>" & HspfChemicalFreundlichExp(1) & "</ChemicalFreundlichExpUpper>")
                sb.AppendLine("    <ChemicalFreundlichExpLower>" & HspfChemicalFreundlichExp(2) & "</ChemicalFreundlichExpLower>")
                sb.AppendLine("    <ChemicalFreundlichExpGround>" & HspfChemicalFreundlichExp(3) & "</ChemicalFreundlichExpGround>")
                sb.AppendLine("    <ChemicalDegradationRateSurface>" & HspfChemicalDegradationRate(0) & "</ChemicalDegradationRateSurface>")
                sb.AppendLine("    <ChemicalDegradationRateUpper>" & HspfChemicalDegradationRate(1) & "</ChemicalDegradationRateUpper>")
                sb.AppendLine("    <ChemicalDegradationRateLower>" & HspfChemicalDegradationRate(2) & "</ChemicalDegradationRateLower>")
                sb.AppendLine("    <ChemicalDegradationRateGround>" & HspfChemicalDegradationRate(3) & "</ChemicalDegradationRateGround>")
                sb.AppendLine("    <OutputInterval>" & [Enum].GetName(HspfOutputInterval.GetType, HspfOutputInterval) & "</OutputInterval>")
                sb.AppendLine("</HSPF>")
            End If

            AppendList("NASSYears", NASSYears, sb)
            sb.AppendLine("<NASSStatistics>" & NASSStatistics & "</NASSStatistics>")

            AppendList("BasinsMetConstituents", BasinsMetConstituents, sb)
            AppendList("WQConstituents", WQConstituents, sb)
            AppendList("NCDCconstituents", NCDCconstituents, sb)
            If D4EM.Data.Source.NCDC.HasToken Then sb.AppendLine("NCDCtoken," & D4EM.Data.Source.NCDC.token)
            AppendList("NLDASconstituents", NLDASconstituents, sb)

            sb.AppendLine("<SoilSource>" & SoilSource & "</SoilSource>")
            sb.AppendLine("<CatchmentsMethod>" & CatchmentsMethod & "</CatchmentsMethod>")
            sb.AppendLine("<ElevationGrid>" & ElevationGrid.Name & "</ElevationGrid>")
            sb.AppendLine("<OverwriteProject>" & OverwriteProject & "</OverwriteProject>")

            'If Not Double.IsNaN(PourPointLatitude + PourPointLongitude + PourPointMaxKm) Then
            '    sb.AppendLine("<PourPoint>")
            '    sb.AppendLine("    <Latitude>" & PourPointLatitude & "</Latitude>")
            '    sb.AppendLine("    <Longitude>" & PourPointLongitude & "</Longitude>")
            '    sb.AppendLine("    <MaxKm>" & PourPointMaxKm & "</MaxKm>")
            '    sb.AppendLine("</PourPoint>")
            'End If

            sb.AppendLine("<NationalLayers>")
            sb.AppendLine("    <NationalHuc8>" & D4EM.Data.National.ShapeFilename(D4EM.Data.National.LayerSpecifications.huc8) & "</NationalHuc8>")
            sb.AppendLine("    <NationalCounty>" & D4EM.Data.National.ShapeFilename(D4EM.Data.National.LayerSpecifications.county) & "</NationalCounty>")
            sb.AppendLine("    <NationalState>" & D4EM.Data.National.ShapeFilename(D4EM.Data.National.LayerSpecifications.state) & "</NationalState>")
            sb.AppendLine("</NationalLayers>")

            For Each lProject In Projects
                sb.Append(lProject.XML)
            Next
            sb.AppendLine("</SDMParameters>")
            Return sb.ToString
        End Get
        Set(ByVal value As String)
            Try
                Dim lDoc As New Xml.XmlDocument
                lDoc.LoadXml(value)
                SetXML(lDoc.FirstChild)
            Catch xex As System.Xml.XmlException
                Logger.Dbg("Exception parsing SDMParameters.XML: " & xex.Message)
            Catch ex As Exception
                Logger.Dbg("Exception setting SDMParameters.XML: " & ex.ToString)
            End Try
        End Set
    End Property
    Private Sub SetXML(ByVal aXML As Xml.XmlNode)
        Dim lArg As Xml.XmlNode = aXML
        While Not lArg Is Nothing
            Select Case lArg.Name
                Case "Project" 'TODO: allow more than one project: Projects.Add(New D4EM.Data.Project(lArg.OuterXml))
                    Project = New D4EM.Data.Project(lArg.OuterXml)
                Case "CreateArcSWATFiles" : CreateArcSWATFiles = Convert.ToBoolean(lArg.InnerText)
                Case "ProjectsPath" : ProjectsPath = lArg.InnerText
                Case "SWAT2005Database" : SWATDatabaseName = lArg.InnerText
                Case "SoilSource" : SoilSource = lArg.InnerText
                Case "MinimumStreamLength" : MinFlowlineKM = Convert.ToDouble(lArg.InnerText)
                Case "MinimumCatchmentArea" : MinCatchmentKM2 = Convert.ToDouble(lArg.InnerText)
                Case "MinumumLandUsePercent" : LandUseIgnoreBelowFraction = Convert.ToDouble(lArg.InnerText) / 100
                Case "NationalHuc8" : D4EM.Data.National.ShapeFilename(D4EM.Data.National.LayerSpecifications.huc8) = lArg.InnerText
                Case "NationalCounty" : D4EM.Data.National.ShapeFilename(D4EM.Data.National.LayerSpecifications.county) = lArg.InnerText
                Case "NationalState" : D4EM.Data.National.ShapeFilename(D4EM.Data.National.LayerSpecifications.state) = lArg.InnerText
                Case "SimulationStartYear" : SimulationStartYear = Convert.ToInt32(lArg.InnerText)
                Case "SimulationEndYear" : SimulationEndYear = Convert.ToInt32(lArg.InnerText)
                Case "HSPF" : SetupHSPF = True : SetXML(lArg.FirstChild)
                Case "SWAT" : SetupSWAT = True : SetXML(lArg.FirstChild)
                Case "OverwriteProject" : Boolean.TryParse(lArg.InnerText, OverwriteProject)
                    'Case "SelectedKeys", "HucList" : SelectedKeys = New Generic.List(Of String)(lArg.InnerText.Split(" "))
                    'Case "Catchments" : Catchments = New Generic.List(Of String)(lValue.Split(" "))
                Case "CatchmentsMethod" : CatchmentsMethod = lArg.InnerText
                    'Case "SelectionLayer", "HucLayer" : SelectionLayer = lArg.InnerText
                    'Case "Latitude" : PourPointLatitude = Convert.ToDouble(lArg.InnerText)
                    'Case "Longitude" : PourPointLongitude = Convert.ToDouble(lArg.InnerText)
                    'Case "MaxKm" : PourPointMaxKm = Convert.ToDouble(lArg.InnerText)
                Case "OutputInterval"
                    [Enum].TryParse(lArg.InnerText, HspfOutputInterval)
                Case "SnowOption" : HspfSnowOption = Convert.ToInt32(lArg.InnerText)
                Case "BacterialOption" : HspfBacterialOption = Convert.ToBoolean(lArg.InnerText)
                Case "ChemicalOption" : HspfChemicalOption = Convert.ToBoolean(lArg.InnerText)
                Case "ChemicalName" : HspfChemicalName = lArg.InnerText
                Case "ChemicalMaximumSolubility" : HspfChemicalMaximumSolubility = Convert.ToDouble(lArg.InnerText)
                Case "ChemicalPartitionCoeffSurface" : HspfChemicalPartitionCoeff(0) = Convert.ToDouble(lArg.InnerText)
                Case "ChemicalPartitionCoeffUpper" : HspfChemicalPartitionCoeff(1) = Convert.ToDouble(lArg.InnerText)
                Case "ChemicalPartitionCoeffLower" : HspfChemicalPartitionCoeff(2) = Convert.ToDouble(lArg.InnerText)
                Case "ChemicalPartitionCoeffGround" : HspfChemicalPartitionCoeff(3) = Convert.ToDouble(lArg.InnerText)
                Case "ChemicalFreundlichExpSurface" : HspfChemicalFreundlichExp(0) = Convert.ToDouble(lArg.InnerText)
                Case "ChemicalFreundlichExpUpper" : HspfChemicalFreundlichExp(1) = Convert.ToDouble(lArg.InnerText)
                Case "ChemicalFreundlichExpLower" : HspfChemicalFreundlichExp(2) = Convert.ToDouble(lArg.InnerText)
                Case "ChemicalFreundlichExpGround" : HspfChemicalFreundlichExp(3) = Convert.ToDouble(lArg.InnerText)
                Case "ChemicalDegradationRateSurface" : HspfChemicalDegradationRate(0) = Convert.ToDouble(lArg.InnerText)
                Case "ChemicalDegradationRateUpper" : HspfChemicalDegradationRate(1) = Convert.ToDouble(lArg.InnerText)
                Case "ChemicalDegradationRateLower" : HspfChemicalDegradationRate(2) = Convert.ToDouble(lArg.InnerText)
                Case "ChemicalDegradationRateGround" : HspfChemicalDegradationRate(3) = Convert.ToDouble(lArg.InnerText)
                Case "NASSYears" : NASSYears = New Generic.List(Of String)(lArg.InnerText.Split(" "))
                Case "NASSStatistics" : NASSStatistics = Convert.ToBoolean(lArg.InnerText)
                Case "BasinsMetConstituents" : BasinsMetConstituents = New Generic.List(Of String)(lArg.InnerText.Split(" "))
                Case "NCDCconstituents" : NCDCconstituents = New Generic.List(Of String)(lArg.InnerText.Split(" "))
                Case "NCDCtoken" : D4EM.Data.Source.NCDC.token = lArg.InnerText
                Case "NLDASconstituents" : NLDASconstituents = New Generic.List(Of String)(lArg.InnerText.Split(" "))
                Case "WQConstituents"
                    WQConstituents = New Generic.List(Of String)
                    Dim lRawItems() As String = lArg.InnerText.Split(",")
                    For Each lConstituent As String In lRawItems
                        WQConstituents.Add(lConstituent.Replace("""", "").Trim())
                    Next
                Case "ElevationGrid"
                    For Each lElevationOption In SDM_Project_Builder_Batch.SDMParameters.ElevationGridOptions
                        If lElevationOption.Name.Equals(lArg.InnerText) Then
                            ElevationGrid = lElevationOption
                            Exit For
                        End If
                    Next

                Case Else
                    If lArg.HasChildNodes Then
                        SetXML(lArg.FirstChild) 'This will find all child nodes in the NextSibling loop
                    End If

                    'MapWinUtility.Logger.Dbg("Did not set variable '" & lArg.Name & "' to '" & lArg.InnerText & "'")

            End Select
            lArg = lArg.NextSibling
        End While
    End Sub

    Public Overrides Function ToString() As String
        Return Me.XML
    End Function

    'Public Function HavePourPoint() As Boolean
    '    Return Not Double.IsNaN(PourPointLatitude + PourPointLongitude + PourPointMaxKm)
    'End Function

    Private Sub AppendList(ByVal VariableName As String, ByVal Values As Generic.List(Of String), ByVal sb As System.Text.StringBuilder)
        If Values IsNot Nothing AndAlso Values.Count > 0 Then
            sb.AppendLine("<" & VariableName & ">" & String.Join(" ", Values.ToArray) & "</" & VariableName & ">")
        End If
    End Sub

    Public Function NewProjectFileName(Optional ByVal aProject As D4EM.Data.Project = Nothing, Optional ByVal aBaseName As String = "NewProject") As String
        Dim lProjectFileFullPath As String

        If aBaseName = "NewProject" Then
            If aProject Is Nothing Then
                If Projects.Count > 0 Then aProject = Project
            End If
            If aProject IsNot Nothing AndAlso aProject.Region IsNot Nothing Then
                Dim lRegionKeys = aProject.Region.GetKeys(aProject.Region.RegionSpecification)
                If aBaseName <> D4EM.Data.Region.MatchAllKeys(0) _
                   AndAlso lRegionKeys.Count > 0 _
                   AndAlso lRegionKeys.Count < 5 Then
                    aBaseName = String.Join("_", lRegionKeys)
                Else
                    aBaseName = aProject.Region.ToString.Replace(" ", "").Replace("Region", "").Replace("NaN", "").Replace("NSWE", "")
                End If
            End If
            aBaseName = SafeFilename(aBaseName)
        End If
        lProjectFileFullPath = IO.Path.Combine(ProjectsPath, aBaseName, aBaseName & ".mwprj")

        Dim lDirName As String = IO.Path.GetDirectoryName(lProjectFileFullPath)
        If FileExists(lDirName, True) AndAlso (IO.Directory.GetFiles(lDirName).Length > 0 _
                                        OrElse IO.Directory.GetDirectories(lDirName).Length > 0) Then
            Dim lSuffix As Integer = 2
            While FileExists(lDirName & "-" & lSuffix, True)  'Find a suffix that will make name unique
                If IO.Directory.GetFiles(lDirName & "-" & lSuffix).Length = 0 AndAlso
                   IO.Directory.GetDirectories(lDirName & "-" & lSuffix).Length = 0 Then
                    Exit While 'Go ahead and use existing empty directory
                End If
                lSuffix += 1
            End While

            lDirName &= "-" & lSuffix
            lProjectFileFullPath = IO.Path.Combine(lDirName, IO.Path.GetFileName(lDirName) & ".mwprj")
        End If

        Logger.Dbg("NewProjectFileName:" & lDirName)
        IO.Directory.CreateDirectory(lDirName)

        Return lProjectFileFullPath
    End Function


End Class
