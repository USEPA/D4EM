Imports atcUtility
Imports MapWinUtility
Imports atcData.atcDataManager

''' <summary>
''' This is an excerpt of atcManDelin from BASINS
''' </summary>
Public Class ManDelinPlugIn

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="aSubBasinThemeName">Name of subbasin shapefile layer</param>
    ''' <param name="aElevationThemeName">Name of elevation grid layer</param>
    ''' <param name="aElevUnits">Elevation Units (Meters, Centimeters, Feet)</param>
    ''' <param name="aRegionName">Name of region, for naming Slope grid that is used for computation and will be created if it does not yet exist</param>
    ''' <remarks></remarks>
    Public Shared Sub CalculateSubbasinParameters(ByVal aSubBasinThemeName As String,
                                                  ByVal aElevationThemeName As String,
                                                  ByVal aElevUnits As String,
                                                  ByVal aRegionName As String)
        Logger.Progress("Calculating SubbasinParameters", 0, 3)

        Dim lSubbasinLayerIndex As Integer = GisUtil.LayerIndex(aSubBasinThemeName)
        Dim lElevationLayerIndex As Integer = GisUtil.LayerIndex(aElevationThemeName)

        'calculate average elev -- this is not actually used anywhere, so why calculate it
        'does mean elev field exist on subbasin shapefile?
        'MeanElevationFieldIndex = GisUtil.FieldIndex(SubbasinLayerIndex, "MEANELEV")
        'If MeanElevationFieldIndex = -1 Then
        '  'need to add it
        '  MeanElevationFieldIndex = GisUtil.AddField(SubbasinLayerIndex, "MEANELEV", 2, 10)
        'End If
        'If GisUtil.LayerType(ElevationLayerIndex) = 3 Then
        '  'shapefile
        '  ElevationFieldIndex = GisUtil.FieldIndex(ElevationLayerIndex, "ELEV_M")
        '  For i = 1 To GisUtil.NumFeatures(SubbasinLayerIndex)
        '    subbasinArea = 0
        '    weightedElev = 0
        '    For j = 1 To GisUtil.NumFeatures(ElevationLayerIndex)
        '      If GisUtil.OverlappingPolygons(ElevationLayerIndex, j - 1, SubbasinLayerIndex, i - 1) Then
        '        subbasinArea = subbasinArea + GisUtil.AreaNthFeatureInLayer(ElevationLayerIndex, j - 1)
        '        weightedElev = weightedElev + (subbasinArea * GisUtil.FieldValue(ElevationLayerIndex, ElevationFieldIndex, j - 1))
        '      End If
        '    Next j
        '    weightedElev = weightedElev / subbasinArea
        '    'store in mean elevation field
        '    GisUtil.SetFeatureValue(SubbasinLayerIndex, MeanElevationFieldIndex, i - 1, weightedElev)
        '  Next i
        'Else
        '  'grid
        'End If

        'Dim lSubbasinFieldIndex As Integer
        'If GisUtil.IsField(lSubbasinLayerIndex, "SUBBASIN") Then
        '    lSubbasinFieldIndex = GisUtil.FieldIndex(lSubbasinLayerIndex, "SUBBASIN")
        'Else 'need to add it
        '    lSubbasinFieldIndex = GisUtil.AddField(lSubbasinLayerIndex, "SUBBASIN", 0, 10)
        '    Logger.Status("Assigning Subbasin Numbers")
        '    For lSubBasinIndex As Integer = 1 To GisUtil.NumFeatures(lSubbasinLayerIndex)
        '        GisUtil.SetFeatureValue(lSubbasinLayerIndex, lSubbasinFieldIndex, lSubBasinIndex - 1, lSubBasinIndex)
        '    Next lSubBasinIndex
        'End If

        'calculate slope
        Dim lSlopeFieldIndex As Integer
        If GisUtil.IsField(lSubbasinLayerIndex, "SLO1") Then
            lSlopeFieldIndex = GisUtil.FieldIndex(lSubbasinLayerIndex, "SLO1")
        Else 'need to add it
            lSlopeFieldIndex = GisUtil.AddField(lSubbasinLayerIndex, "SLO1", 2, 10)
        End If

        Using lLevel As New ProgressLevel(True)
            Dim lSlope As Double
            If GisUtil.LayerType(lElevationLayerIndex) = 3 Then 'shapefile
                Logger.Status("Calculating Slope from Elevation Shapefile")
                Dim lSubbasinCount As Integer = GisUtil.NumFeatures(lSubbasinLayerIndex)
                Dim lElevationShapeCount As Integer = GisUtil.NumFeatures(lElevationLayerIndex)
                Dim lElevationFieldIndex As Integer = GisUtil.FieldIndex(lElevationLayerIndex, "ELEV_M")
                Dim lElevationIndex(lElevationShapeCount) As Integer
                GisUtil.AssignContainingPolygons(lElevationLayerIndex, lSubbasinLayerIndex, lElevationIndex)
                For lSubbasinIndex As Integer = 1 To lSubbasinCount
                    Logger.Progress(lSubbasinIndex, lSubbasinCount)
                    Dim lElevMin As Double = 99999999
                    Dim lElevMax As Double = -99999999
                    Dim lElev As Double
                    For lElevationShapeIndex As Integer = 1 To lElevationShapeCount
                        'npercent = 100 * i * j / ntot
                        'If npercent > lastpercent Then
                        '  lblCalc.Text = "Calculating (" & npercent & "%)"
                        '  lastpercent = npercent
                        '  Me.Refresh()
                        'End If
                        If lElevationIndex(lElevationShapeIndex) = lSubbasinIndex - 1 Then
                            lElev = GisUtil.FieldValue(lElevationLayerIndex, lElevationShapeIndex - 1, lElevationFieldIndex)
                            If lElev > lElevMax Then
                                lElevMax = lElev
                            End If
                            If lElev < lElevMin Then
                                lElevMin = lElev
                            End If
                        End If
                    Next lElevationShapeIndex
                    'store in slope field as percent
                    'estimate slope as the difference between max and min elevations / square root of subbasin area -- better approx?
                    lSlope = (lElevMax - lElevMin) / ((GisUtil.FeatureArea(lSubbasinLayerIndex, lSubbasinIndex - 1)) ^ 0.5)
                    If aElevUnits = "Meters" Then
                        lSlope *= 100
                    ElseIf aElevUnits = "Feet" Then
                        lSlope = lSlope * 100 / 3.281
                    End If
                    GisUtil.SetFeatureValue(lSubbasinLayerIndex, lSlopeFieldIndex, lSubbasinIndex - 1, lSlope)
                Next lSubbasinIndex
            Else 'grid
                Logger.Status("Calculating Slope from Elevation Grid")

                Dim lInputGridName As String = GisUtil.LayerFileName(lElevationLayerIndex)
                Dim lSlopeGridFileName As String = IO.Path.Combine(PathNameOnly(lInputGridName), "slope_" & SafeFilename(aRegionName.Replace(" ", ""), "") & ".tif")
                Dim lSlopeLayer = D4EM.Geo.SpatialOperations.GetSlopeGrid(GisUtil.Layers(lElevationLayerIndex), lSlopeGridFileName) ', lRegion)

                Dim lSubbasinCount As Integer = GisUtil.NumFeatures(lSubbasinLayerIndex)
                For lSubbasinIndex As Integer = 1 To lSubbasinCount
                    Logger.Progress(lSubbasinIndex, lSubbasinCount)
                    'store in slope field as percent
                    If GisUtil.FieldValue(lSubbasinLayerIndex, lSubbasinIndex - 1, lSlopeFieldIndex) <= 0 Then
                        lSlope = GisUtil.GridMeanInPolygon(lSlopeLayer, lSubbasinLayerIndex, lSubbasinIndex - 1)

                        'If slope grid is unitless values between 0 and 1
                        'If aElevUnits = "Meters" Then
                        '    lSlope *= 100
                        'ElseIf aElevUnits = "Feet" Then
                        '    lSlope = lSlope * 100 / 3.281
                        'End If

                        'If slope grid is percent
                        If aElevUnits = "Feet" Then
                            lSlope /= 3.281
                        End If

                        GisUtil.SetFeatureValue(lSubbasinLayerIndex, lSlopeFieldIndex, lSubbasinIndex - 1, lSlope)
                    End If
                Next lSubbasinIndex
                'D4EM.Geo.SpatialOperations.GridShapeIDLayer.Close()
                'D4EM.Geo.SpatialOperations.GridShapeIDLayer.Dispose()
            End If
        End Using

        'calculate length of overland flow plane
        'this is computed in WinHSPF based on slope, no need to compute here
        'Dim sl As Double

        'If GisUtil.IsField(SubbasinLayerIndex, "LEN1") Then
        '  LengthFieldIndex = GisUtil.FieldIndex(SubbasinLayerIndex, "LEN1")
        'Else
        '  'need to add it
        '  LengthFieldIndex = GisUtil.AddField(SubbasinLayerIndex, "LEN1", 2, 10)
        'End If
        'For i = 1 To GisUtil.NumFeatures(SubbasinLayerIndex)
        '  slope = GisUtil.FieldValue(SubbasinLayerIndex, i - 1, SlopeFieldIndex)
        '  'Slope Length from old autodelin
        '  If ((slope > 0) And (slope < 2.0)) Then
        '    sl = 400 / 3.28
        '  ElseIf ((slope >= 2.0) And (slope < 5.0)) Then
        '    sl = 300 / 3.28
        '  ElseIf ((slope >= 5.0) And (slope < 8.0)) Then
        '    sl = 200 / 3.28
        '  ElseIf ((slope >= 8) And (slope < 10.0)) Then
        '    sl = 200 / 3.28
        '  ElseIf ((slope >= 10) And (slope < 12.0)) Then
        '    sl = 120.0 / 3.28
        '  ElseIf ((slope >= 12) And (slope < 16.0)) Then
        '    sl = 80.0 / 3.28
        '  ElseIf ((slope >= 16) And (slope < 20.0)) Then
        '    sl = 60.0 / 3.28
        '  ElseIf ((slope >= 20) And (slope < 25.0)) Then
        '    sl = 50.0 / 3.28
        '  Else
        '    sl = 0.05  '30.0/3.28      
        '  End If
        '  GisUtil.SetFeatureValue(SubbasinLayerIndex, LengthFieldIndex, i - 1, sl)
        'Next i

        Using lLevel As New ProgressLevel(True)
            'set area of each subbasin
            Logger.Status("Calculating Areas")
            Dim lAreaAcresFieldIndex As Integer
            If GisUtil.IsField(lSubbasinLayerIndex, "AREAACRES") Then
                lAreaAcresFieldIndex = GisUtil.FieldIndex(lSubbasinLayerIndex, "AREAACRES")
            Else 'need to add it
                lAreaAcresFieldIndex = GisUtil.AddField(lSubbasinLayerIndex, "AREAACRES", 2, 10)
            End If
            Dim lAreaMi2FieldIndex As Integer
            If GisUtil.IsField(lSubbasinLayerIndex, "AREAMI2") Then
                lAreaMi2FieldIndex = GisUtil.FieldIndex(lSubbasinLayerIndex, "AREAMI2")
            Else 'need to add it
                lAreaMi2FieldIndex = GisUtil.AddField(lSubbasinLayerIndex, "AREAMI2", 2, 10)
            End If
            For lSubbasinsIndex As Integer = 1 To GisUtil.NumFeatures(lSubbasinLayerIndex)
                Dim lArea As Double = GisUtil.FeatureArea(lSubbasinLayerIndex, lSubbasinsIndex - 1)
                GisUtil.SetFeatureValue(lSubbasinLayerIndex, lAreaAcresFieldIndex, lSubbasinsIndex - 1, lArea / 4046.86)
                GisUtil.SetFeatureValue(lSubbasinLayerIndex, lAreaMi2FieldIndex, lSubbasinsIndex - 1, lArea / 2589988)
            Next lSubbasinsIndex
            GisUtil.Layers(lSubbasinLayerIndex).AsFeatureSet.Save()
        End Using
        Logger.Status("")
    End Sub

    ''' <summary>
    ''' Assign subbasin numbers to each reach segment
    ''' </summary>
    ''' <param name="aStreamsLayerName"></param>
    ''' <param name="aSubbasinLayerName"></param>
    ''' <param name="aMinField"></param>
    ''' <remarks></remarks>
    Public Shared Sub CalculateReachSubbasinIds(ByVal aStreamsLayerName As String, _
                                                ByVal aSubbasinLayerName As String, _
                                                ByRef aMinField As Integer)

        Dim lStreamsLayerIndex As Integer = GisUtil.LayerIndex(aStreamsLayerName)
        Dim lSubbasinLayerIndex As Integer = GisUtil.LayerIndex(aSubbasinLayerName)
        Dim lNumStreams As Integer = GisUtil.NumFeatures(lStreamsLayerIndex)
        Dim lNumSubbasins As Integer = GisUtil.NumFeatures(lSubbasinLayerIndex)
        Dim lSubbasinFieldIndex As Integer = GisUtil.FieldIndex(lSubbasinLayerIndex, "SUBBASIN")
        Dim lStreamSubbasinFieldIndex As Integer = GisUtil.FieldIndexAddIfMissing(lStreamsLayerIndex, "SUBBASIN", 1, 10)

        Try 'Use existing mapping between streams and subbasins
            Dim lWatershedOrigIdFieldIndex As Integer = GisUtil.FieldIndex(lSubbasinLayerIndex, "Watershed")
            Dim lStreamOrigIdFieldIndex As Integer = GisUtil.FieldIndex(lStreamsLayerIndex, "LINKNO")

            GisUtil.StartSetFeatureValue(lStreamsLayerIndex)
            For lStreamIndex As Integer = 1 To lNumStreams
                Dim lStreamOrigID As String = GisUtil.FieldValue(lStreamsLayerIndex, lStreamIndex - 1, lStreamOrigIdFieldIndex)
                For lBasinIndex As Integer = 1 To lNumSubbasins
                    If lStreamOrigID = GisUtil.FieldValue(lSubbasinLayerIndex, lBasinIndex - 1, lWatershedOrigIdFieldIndex) Then
                        GisUtil.SetFeatureValueNoStartStop(lStreamsLayerIndex, lStreamSubbasinFieldIndex, lStreamIndex - 1,
                                       GisUtil.FieldValue(lSubbasinLayerIndex, lBasinIndex - 1, lSubbasinFieldIndex))
                        Exit For
                    End If
                Next
            Next

            'If we have downstream information, translate it into new SUBBASIN/SUBBASINR values
            Dim lStreamOrigDownstreamFieldIndex As Integer = -1
            If GisUtil.IsField(lStreamsLayerIndex, "DSLINKNO") Then
                lStreamOrigDownstreamFieldIndex = GisUtil.FieldIndex(lStreamsLayerIndex, "DSLINKNO")
                Dim lStreamDownstreamFieldIndex As Integer = GisUtil.FieldIndexAddIfMissing(lStreamsLayerIndex, "SUBBASINR", 1, 10)
                For lStreamIndex As Integer = 1 To lNumStreams
                    Dim lStreamOrigDownID As String = GisUtil.FieldValue(lStreamsLayerIndex, lStreamIndex - 1, lStreamOrigDownstreamFieldIndex).Trim()
                    Dim lNewDownID As Integer = -999
                    If IsNumeric(lStreamOrigDownID) AndAlso lStreamOrigDownID >= 0 Then
                        For lOtherStreamIndex As Integer = 1 To lNumStreams
                            If lOtherStreamIndex <> lStreamIndex AndAlso _
                               GisUtil.FieldValue(lStreamsLayerIndex, lOtherStreamIndex - 1, lStreamOrigIdFieldIndex).Trim() = lStreamOrigDownID Then
                                lNewDownID = GisUtil.FieldValue(lStreamsLayerIndex, lOtherStreamIndex - 1, lStreamSubbasinFieldIndex)
                                Exit For
                            End If
                        Next
                    End If
                    GisUtil.SetFeatureValueNoStartStop(lStreamsLayerIndex, lStreamDownstreamFieldIndex, lStreamIndex - 1, lNewDownID)
                Next
            End If

            GisUtil.StopSetFeatureValue(lStreamsLayerIndex)
        Catch
            'compute mapping between streams and subbasins
            Dim aIndex(GisUtil.NumFeatures(lStreamsLayerIndex)) As Integer
            GisUtil.AssignContainingPolygons(lStreamsLayerIndex, lSubbasinLayerIndex, aIndex)
            GisUtil.StartSetFeatureValue(lStreamsLayerIndex)
            For i As Integer = 1 To lNumStreams
                Dim j As Integer
                If aIndex(i) > -1 Then
                    j = GisUtil.FieldValue(lSubbasinLayerIndex, aIndex(i), lSubbasinFieldIndex)
                Else
                    j = aIndex(i)
                End If
                GisUtil.SetFeatureValueNoStartStop(lStreamsLayerIndex, lStreamSubbasinFieldIndex, i - 1, j)
            Next i
            GisUtil.StopSetFeatureValue(lStreamsLayerIndex)
        End Try

        If lStreamSubbasinFieldIndex < aMinField Then aMinField = lStreamSubbasinFieldIndex
        GisUtil.Layers(lStreamsLayerIndex).AsFeatureSet.Save()
    End Sub

    Public Shared Sub CalculateReachDownstreamSubbasinIds(ByVal aStreamsLayerName As String, _
                                                          ByRef aMinField As Integer)
        'add downstream subbasin ids
        Dim lStreamsLayerIndex As Integer = GisUtil.LayerIndex(aStreamsLayerName)
        If GisUtil.IsField(lStreamsLayerIndex, "SUBBASINR") Then
            Logger.Dbg("Already have SUBBASINR field")
        Else
            Dim lReachSubbasinFieldIndex As Integer = GisUtil.FieldIndex(lStreamsLayerIndex, "SUBBASIN")
            Dim lNumStreams As Integer = GisUtil.NumFeatures(lStreamsLayerIndex)

            Dim lReachField As Integer
            If GisUtil.IsField(lStreamsLayerIndex, "RIVRCH") Then
                lReachField = GisUtil.FieldIndex(lStreamsLayerIndex, "RIVRCH")
            ElseIf GisUtil.IsField(lStreamsLayerIndex, "RCHID") Then
                lReachField = GisUtil.FieldIndex(lStreamsLayerIndex, "RCHID")
            ElseIf GisUtil.IsField(lStreamsLayerIndex, "COMID") Then
                lReachField = GisUtil.FieldIndex(lStreamsLayerIndex, "COMID")
            ElseIf GisUtil.IsField(lStreamsLayerIndex, "SUBBASIN") Then
                lReachField = GisUtil.FieldIndex(lStreamsLayerIndex, "SUBBASIN")
            End If
            Dim lDownReachField As Integer
            If GisUtil.IsField(lStreamsLayerIndex, "DSCSM") Then
                lDownReachField = GisUtil.FieldIndex(lStreamsLayerIndex, "DSCSM")
            ElseIf GisUtil.IsField(lStreamsLayerIndex, "DSRCHID") Then
                lDownReachField = GisUtil.FieldIndex(lStreamsLayerIndex, "DSRCHID")
            ElseIf GisUtil.IsField(lStreamsLayerIndex, "TOCOMID") Then
                lDownReachField = GisUtil.FieldIndex(lStreamsLayerIndex, "TOCOMID")
            ElseIf GisUtil.IsField(lStreamsLayerIndex, "SUBBASINR") Then
                lDownReachField = GisUtil.FieldIndex(lStreamsLayerIndex, "SUBBASINR")
            End If

            Dim lDownstreamFieldIndex As Integer = GisUtil.FieldIndexAddIfMissing(lStreamsLayerIndex, "SUBBASINR", 1, 10)
            If lDownstreamFieldIndex < aMinField Then aMinField = lDownstreamFieldIndex

            Dim rval As String
            Dim dval As String
            Dim dsubbasin As String
            Dim rsubbasin As String
            Dim i As Integer
            GisUtil.StartSetFeatureValue(lStreamsLayerIndex)
            'populate the downstream subbasin ids
            For i = 1 To lNumStreams
                Logger.Progress(i, lNumStreams)
                System.Windows.Forms.Application.DoEvents()
                dval = GisUtil.FieldValue(lStreamsLayerIndex, i - 1, lDownReachField)
                'find what is downstream of rval
                For lSteamIndexDown As Integer = 1 To lNumStreams
                    rval = GisUtil.FieldValue(lStreamsLayerIndex, lSteamIndexDown - 1, lReachField)
                    If rval = dval Then
                        'this is the downstream segment
                        dsubbasin = GisUtil.FieldValue(lStreamsLayerIndex, lSteamIndexDown - 1, lReachSubbasinFieldIndex)
                        rsubbasin = GisUtil.FieldValue(lStreamsLayerIndex, i - 1, lReachSubbasinFieldIndex)
                        'if the downstream subbasin id is different that this subbasin id
                        'set it, and make the same change to all segments of this subbasin id
                        If dsubbasin <> rsubbasin Then
                            GisUtil.SetFeatureValueNoStartStop(lStreamsLayerIndex, lDownstreamFieldIndex, i - 1, dsubbasin)
                            'make another pass to set each stream within a subbasin to the same subbasinr
                            For lStreamIndex As Integer = 1 To lNumStreams
                                If GisUtil.FieldValue(lStreamsLayerIndex, lStreamIndex - 1, lReachSubbasinFieldIndex) = rsubbasin Then
                                    GisUtil.SetFeatureValueNoStartStop(lStreamsLayerIndex, lDownstreamFieldIndex, lStreamIndex - 1, dsubbasin)
                                End If
                            Next lStreamIndex
                        End If
                        'exit once we found what is downstream of this segment
                        Exit For
                    End If
                Next lSteamIndexDown
            Next i
            For i = 1 To lNumStreams
                Logger.Progress(i, lNumStreams)
                System.Windows.Forms.Application.DoEvents()
                dval = GisUtil.FieldValue(lStreamsLayerIndex, i - 1, lDownstreamFieldIndex)
                If dval = 0 Then
                    GisUtil.SetFeatureValueNoStartStop(lStreamsLayerIndex, lDownstreamFieldIndex, i - 1, -999)
                End If
            Next i
            Logger.Progress(lNumStreams, lNumStreams)
            GisUtil.StopSetFeatureValue(lStreamsLayerIndex)
        End If
    End Sub

    Public Shared Sub CalculateReachParameters(ByVal aStreamsLayerName As String, _
                                               ByVal aSubbasinThemeName As String, _
                                               ByVal aElevationThemeName As String, _
                                               ByVal aElevUnits As String, _
                                               ByRef aMinField As Integer)

        'create and populate fields
        Logger.Status("Calculating attributes...")

        Dim lStreamsLayerIndex As Integer = GisUtil.LayerIndex(aStreamsLayerName)
        Dim lSubbasinLayerIndex As Integer = GisUtil.LayerIndex(aSubbasinThemeName)

        Dim lSubbasinFieldIndex As Integer = GisUtil.FieldIndex(lSubbasinLayerIndex, "SUBBASIN")
        Dim lReachSubbasinFieldIndex As Integer = GisUtil.FieldIndexAddIfMissing(lStreamsLayerIndex, "SUBBASIN", 1, 10)
        Dim lDownstreamFieldIndex As Integer = GisUtil.FieldIndexAddIfMissing(lStreamsLayerIndex, "SUBBASINR", 1, 10)

        Dim lNumStreams As Integer = GisUtil.NumFeatures(lStreamsLayerIndex)

        'set length of stream reach
        Dim lLengthFieldIndex As Integer = GisUtil.FieldIndexAddIfMissing(lStreamsLayerIndex, "LEN2", 2, 10)
        If lLengthFieldIndex < aMinField Then aMinField = lLengthFieldIndex
        Dim r As Double
        Dim i As Integer
        For i = 1 To lNumStreams
            r = GisUtil.FeatureLength(lStreamsLayerIndex, i - 1)
            GisUtil.SetFeatureValue(lStreamsLayerIndex, lLengthFieldIndex, i - 1, r)
        Next i

        'set local contributing area of stream reach
        Dim rval As String
        Dim dval As String
        Dim AreaFieldIndex As Integer = GisUtil.FieldIndexAddIfMissing(lStreamsLayerIndex, "LAREA", 2, 10)
        If AreaFieldIndex < aMinField Then aMinField = AreaFieldIndex
        For i = 1 To lNumStreams
            rval = GisUtil.FieldValue(lStreamsLayerIndex, i - 1, lReachSubbasinFieldIndex)
            For lSubbasinIndex As Integer = 1 To GisUtil.NumFeatures(lSubbasinLayerIndex)
                dval = GisUtil.FieldValue(lSubbasinLayerIndex, lSubbasinIndex - 1, lSubbasinFieldIndex)
                If dval = rval Then
                    r = GisUtil.FeatureArea(lSubbasinLayerIndex, lSubbasinIndex - 1)
                    GisUtil.SetFeatureValue(lStreamsLayerIndex, AreaFieldIndex, i - 1, r)
                    Exit For
                End If
            Next lSubbasinIndex
        Next i

        'set total contributing area of stream reach
        Dim bfound As Boolean
        Dim r2 As Double
        Dim tAreaFieldIndex As Integer
        tAreaFieldIndex = GisUtil.FieldIndexAddIfMissing(lStreamsLayerIndex, "TAREA", 2, 20)
        If tAreaFieldIndex < aMinField Then aMinField = tAreaFieldIndex
        For i = 1 To lNumStreams
            r = GisUtil.FieldValue(lStreamsLayerIndex, i - 1, AreaFieldIndex)
            GisUtil.SetFeatureValue(lStreamsLayerIndex, tAreaFieldIndex, i - 1, r)
        Next i
        For i = 1 To lNumStreams
            Logger.Progress(i, lNumStreams)
            System.Windows.Forms.Application.DoEvents()
            r2 = GisUtil.FieldValue(lStreamsLayerIndex, i - 1, AreaFieldIndex)                        'local area of this one
            rval = GisUtil.FieldValue(lStreamsLayerIndex, i - 1, lReachSubbasinFieldIndex)
            'Logger.Dbg("ManDelin:adding area from feature " & rval)
            'is there anything downstream of this one?
            dval = GisUtil.FieldValue(lStreamsLayerIndex, i - 1, lDownstreamFieldIndex)
            Dim lAllDownstreamIndexes As New List(Of String)
            lAllDownstreamIndexes.Add(rval)
            Do While dval > 0
                Logger.Dbg("ManDelin:" & dval & " downstream of " & rval)
                If lAllDownstreamIndexes.Contains(dval) Then
                    Logger.Dbg("Stream Error: Circular downstream found.")
                    Exit Do
                End If
                lAllDownstreamIndexes.Add(dval)
                bfound = False
                For lStreamIndexDownstream As Integer = 1 To lNumStreams
                    rval = GisUtil.FieldValue(lStreamsLayerIndex, lStreamIndexDownstream - 1, lReachSubbasinFieldIndex)
                    If rval = dval Then 'this is the one
                        r = GisUtil.FieldValue(lStreamsLayerIndex, lStreamIndexDownstream - 1, tAreaFieldIndex)   'total area of downstream one
                        GisUtil.SetFeatureValue(lStreamsLayerIndex, tAreaFieldIndex, lStreamIndexDownstream - 1, r + r2)
                        'Logger.Dbg("ManDelin:" & rval & " area now " & r + r2)
                        dval = GisUtil.FieldValue(lStreamsLayerIndex, lStreamIndexDownstream - 1, lDownstreamFieldIndex)
                        bfound = True
                        Exit For
                    End If
                Next lStreamIndexDownstream
                If Not bfound Then
                    dval = 0
                End If
            Loop
        Next i
        'add total contributing area in acres and square miles
        Dim AreaAcresFieldIndex As Integer = GisUtil.FieldIndexAddIfMissing(lStreamsLayerIndex, "TAREAACRES", 2, 20)
        Dim AreaMi2FieldIndex As Integer = GisUtil.FieldIndexAddIfMissing(lStreamsLayerIndex, "TAREAMI2", 2, 10)
        For i = 1 To lNumStreams
            r = GisUtil.FieldValue(lStreamsLayerIndex, i - 1, tAreaFieldIndex)
            GisUtil.SetFeatureValue(lStreamsLayerIndex, AreaAcresFieldIndex, i - 1, r / 4046.86)
            GisUtil.SetFeatureValue(lStreamsLayerIndex, AreaMi2FieldIndex, i - 1, r / 2589988)
        Next i

        'set stream width based on upstream area
        Dim lFieldIndex As Integer = GisUtil.FieldIndexAddIfMissing(lStreamsLayerIndex, "WID2", 2, 10)
        If lFieldIndex < aMinField Then aMinField = lFieldIndex
        For i = 1 To lNumStreams
            r = GisUtil.FieldValue(lStreamsLayerIndex, i - 1, tAreaFieldIndex)
            r2 = (1.29) * ((r / 1000000) ^ (0.6))
            GisUtil.SetFeatureValue(lStreamsLayerIndex, lFieldIndex, i - 1, r2)
        Next i

        'set depth based on upstream area
        lFieldIndex = GisUtil.FieldIndexAddIfMissing(lStreamsLayerIndex, "DEP2", 2, 10)
        If lFieldIndex < aMinField Then aMinField = lFieldIndex
        For i = 1 To lNumStreams
            r = GisUtil.FieldValue(lStreamsLayerIndex, i - 1, tAreaFieldIndex)
            r2 = (0.13) * ((r / 1000000) ^ (0.4))
            GisUtil.SetFeatureValue(lStreamsLayerIndex, lFieldIndex, i - 1, r2)
        Next i

        'set min elev
        Dim lMinElevFieldIndex As Integer = GisUtil.FieldIndexAddIfMissing(lStreamsLayerIndex, "MINEL", 1, 10)
        If lMinElevFieldIndex < aMinField Then aMinField = lMinElevFieldIndex
        'set max elev
        Dim lMaxElevFieldIndex As Integer = GisUtil.FieldIndexAddIfMissing(lStreamsLayerIndex, "MAXEL", 1, 10)
        If lMaxElevFieldIndex < aMinField Then aMinField = lMaxElevFieldIndex

        Dim x1 As Double
        Dim x2 As Double
        Dim y1 As Double
        Dim y2 As Double
        Dim gmin As Integer
        Dim gmax As Integer
        Dim gtemp As Integer
        Dim lElevationLayerIndex As Integer = GisUtil.LayerIndex(aElevationThemeName)
        Dim lElevationLayer = GisUtil.Layers(lElevationLayerIndex)
        Dim lElevationGrid As DotSpatial.Data.Raster = Nothing
        Dim lRCindex As DotSpatial.Data.RcIndex

        For i = 1 To lNumStreams
            'return end points of stream segment
            GisUtil.EndPointsOfLine(lStreamsLayerIndex, i - 1, x1, y1, x2, y2)
            If GisUtil.LayerType(lElevationLayerIndex) = 3 Then
                'get shapefile value at point
                Dim lElevation As Integer = GisUtil.PointInPolygonXY(x1, y1, lElevationLayerIndex)
                Dim lElevationFieldIndex As Integer = GisUtil.FieldIndex(lElevationLayerIndex, "ELEV_M")
                gmin = GisUtil.FieldValue(lElevationLayerIndex, lElevation, lElevationFieldIndex)
                lElevation = GisUtil.PointInPolygonXY(x2, y2, lElevationLayerIndex)
                gmax = GisUtil.FieldValue(lElevationLayerIndex, lElevation, lElevationFieldIndex)
            Else 'get grid value at point
                'gmin = GisUtil.GridValueAtPoint(lElevationLayerIndex, x1, y1)
                'gmax = GisUtil.GridValueAtPoint(lElevationLayerIndex, x2, y2)
                If lElevationGrid Is Nothing Then
                    lElevationGrid = lElevationLayer.AsRaster()
                End If
                lRCindex = DotSpatial.Data.RasterExt.ProjToCell(lElevationGrid, x1, y1)
                If lRCindex.IsEmpty Then
                    Logger.Dbg("Elevation grid does not cover bottom of stream " & i & ": " & DoubleToString(x1) & ", " & DoubleToString(y1))
                Else
                    gmin = Geo.SpatialOperations.ValidGridValueNear(lElevationGrid, lRCindex, -100000, 1000000)
                End If
                lRCindex = DotSpatial.Data.RasterExt.ProjToCell(lElevationGrid, x2, y2)
                If lRCindex.IsEmpty Then
                    Logger.Dbg("Elevation grid does not cover top of stream " & i & ": " & DoubleToString(x1) & ", " & DoubleToString(y1))
                Else
                    gmax = Geo.SpatialOperations.ValidGridValueNear(lElevationGrid, lRCindex, -100000, 1000000)
                End If
            End If
            If aElevUnits = "Centimeters" Then
                gmin /= 100  'this is an ned grid (in cm), convert to meters
                gmax /= 100
            ElseIf aElevUnits = "Feet" Then
                gmin /= 3.281  'this is a grid in ft, convert to meters
                gmax /= 3.281
            End If
            If gmax < gmin Then
                gtemp = gmin
                gmin = gmax
                gmax = gtemp
            End If
            GisUtil.SetFeatureValue(lStreamsLayerIndex, lMinElevFieldIndex, i - 1, gmin)
            GisUtil.SetFeatureValue(lStreamsLayerIndex, lMaxElevFieldIndex, i - 1, gmax)
        Next i

        'set slope of stream reach
        lFieldIndex = GisUtil.FieldIndexAddIfMissing(lStreamsLayerIndex, "SLO2", 2, 10)
        If lFieldIndex < aMinField Then aMinField = lFieldIndex
        For i = 1 To lNumStreams
            gmin = GisUtil.FieldValue(lStreamsLayerIndex, i - 1, lMinElevFieldIndex)
            gmax = GisUtil.FieldValue(lStreamsLayerIndex, i - 1, lMaxElevFieldIndex)
            gtemp = GisUtil.FieldValue(lStreamsLayerIndex, i - 1, lLengthFieldIndex)
            Try
                GisUtil.SetFeatureValue(lStreamsLayerIndex, lFieldIndex, i - 1, (gmax - gmin) * 100 / gtemp)
            Catch e As Exception
                Logger.Dbg("Unable to calculate slope for stream index " & lStreamsLayerIndex & " " & e.Message)
                GisUtil.SetFeatureValue(lStreamsLayerIndex, lFieldIndex, i - 1, 100)
            End Try
        Next i

        'set name of each stream reach
        lFieldIndex = GisUtil.FieldIndexAddIfMissing(lStreamsLayerIndex, "SNAME", 0, 20)
        If lFieldIndex < aMinField Then aMinField = lFieldIndex
        Dim NameFieldIndex As Integer
        Dim Name As String
        If GisUtil.IsField(lStreamsLayerIndex, "PNAME") Then
            NameFieldIndex = GisUtil.FieldIndex(lStreamsLayerIndex, "PNAME")
        ElseIf GisUtil.IsField(lStreamsLayerIndex, "NAME") Then
            NameFieldIndex = GisUtil.FieldIndex(lStreamsLayerIndex, "NAME")
        ElseIf GisUtil.IsField(lStreamsLayerIndex, "GNIS_NAME") Then
            NameFieldIndex = GisUtil.FieldIndex(lStreamsLayerIndex, "GNIS_NAME")
        End If
        If NameFieldIndex > -1 Then
            For i = 1 To lNumStreams
                Name = GisUtil.FieldValue(lStreamsLayerIndex, i - 1, NameFieldIndex)
                GisUtil.SetFeatureValue(lStreamsLayerIndex, lFieldIndex, i - 1, Name)
            Next i
        End If
        'add name to subbasin layer as well
        NameFieldIndex = GisUtil.FieldIndexAddIfMissing(lSubbasinLayerIndex, "BNAME", 0, 20)
        For i = 1 To GisUtil.NumFeatures(lSubbasinLayerIndex)
            dval = GisUtil.FieldValue(lSubbasinLayerIndex, i - 1, lSubbasinFieldIndex)
            For lSteamIndex As Integer = 1 To lNumStreams
                rval = GisUtil.FieldValue(lStreamsLayerIndex, lSteamIndex - 1, lReachSubbasinFieldIndex)
                If rval = dval Then
                    'this is the one
                    If Len(Trim(GisUtil.FieldValue(lSubbasinLayerIndex, i - 1, NameFieldIndex))) = 0 Then
                        GisUtil.SetFeatureValue(lSubbasinLayerIndex, NameFieldIndex, i - 1, GisUtil.FieldValue(lStreamsLayerIndex, lSteamIndex - 1, lFieldIndex))
                        Exit For
                    End If
                End If
            Next lSteamIndex
        Next i
        GisUtil.Layers(lStreamsLayerIndex).AsFeatureSet.Save()
        GisUtil.Layers(lSubbasinLayerIndex).AsFeatureSet.Save()
        Logger.Status("")
    End Sub
End Class