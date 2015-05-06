Imports DotSpatial.Projections
Imports System.Collections.Generic
Imports System.Xml
Imports D4EM.Data
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports D4EM.Data.Source

'''<summary>
'''This is a test class for USGS_SeamlessTest and is intended
'''to contain all USGS_SeamlessTest Unit Tests
'''</summary>
<TestClass()> _
Public Class USGS_SeamlessTest
    Private testContextInstance As TestContext

    '''<summary>
    '''Gets or sets the test context which provides
    '''information about and functionality for the current test run.
    '''</summary>
    Public Property TestContext() As TestContext
        Get
            Return testContextInstance
        End Get
        Set(ByVal value As TestContext)
            testContextInstance = Value
        End Set
    End Property

#Region "Additional test attributes"
    '
    'You can use the following additional attributes as you write your tests:
    '
    'Use ClassInitialize to run code before running the first test in the class
    '<ClassInitialize()>  _
    'Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
    'End Sub
    '
    'Use ClassCleanup to run code after all tests in a class have run
    '<ClassCleanup()>  _
    'Public Shared Sub MyClassCleanup()
    'End Sub
    '
    'Use TestInitialize to run code before running each test
    '<TestInitialize()>  _
    'Public Sub MyTestInitialize()
    'End Sub
    '
    'Use TestCleanup to run code after each test has run
    '<TestCleanup()>  _
    'Public Sub MyTestCleanup()
    'End Sub
    '
#End Region

    '''<summary>Test for QuerySchema</summary>
    <TestMethod()> Public Sub QuerySchemaTest()
        Dim lUSGS_Seamless As USGS_Seamless = New USGS_Seamless()
        Dim lQuerySchema As String = lUSGS_Seamless.QuerySchema
        Assert.IsTrue(lQuerySchema.Contains("<DataExtension name=""NLCD2001"">"))
        Assert.IsTrue(lQuerySchema.Contains("<function name=""GetNLCD2001"""))
    End Sub

    '''<summary>Test for Name</summary>
    <TestMethod()> Public Sub NameTest()
        Dim lUSGS_Seamless As USGS_Seamless = New USGS_Seamless()
        Assert.AreEqual("D4EM Data Download::USGS_Seamless", lUSGS_Seamless.Name)
    End Sub

    '''<summary>Test for Author</summary>
    <TestMethod()> Public Sub DescriptionTest()
        Dim lUSGS_Seamless As USGS_Seamless = New USGS_Seamless()
        Assert.AreEqual("Retrieve NLCD and other data from USGS Seamless", lUSGS_Seamless.Description)
    End Sub

    '''<summary>Test for Author</summary>
    <TestMethod()> Public Sub AuthorTest()
        Dim lUSGS_Seamless As USGS_Seamless = New USGS_Seamless()
        Dim lExpected As String = "U.S. Environmental Protection Agency" + Environment.NewLine _
                                & "Office of Research and Development" + Environment.NewLine _
                                & "Ecosystems Research Division"
        Assert.AreEqual(lExpected, lUSGS_Seamless.Author)
    End Sub

    '''<summary>Test for StripHTMLtags</summary>
    <TestMethod(), DeploymentItem("D4EM.Data.Source.USGS_Seamless.dll")> Public Sub StripHTMLtagsTest()
        Dim aOriginal As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = USGS_Seamless_Accessor.StripHTMLtags(aOriginal)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for ProcessDownloadedZip</summary>
    <TestMethod(), DeploymentItem("D4EM.Data.Source.USGS_Seamless.dll")> Public Sub ProcessDownloadedZipTest()
        'Dim aCacheZipFile As String = String.Empty ' TODO: Initialize to an appropriate value
        'Dim aDefaultString As String = String.Empty ' TODO: Initialize to an appropriate value
        'Dim aDataType As LayerSpecification = Nothing ' TODO: Initialize to an appropriate value
        'Dim aSaveFolder As String = String.Empty ' TODO: Initialize to an appropriate value
        'Dim aBaseFilename As String = String.Empty ' TODO: Initialize to an appropriate value
        'Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        'Dim actual As String
        'actual = USGS_Seamless_Accessor.ProcessDownloadedZip(aCacheZipFile, aDefaultString, aDataType, aSaveFolder, aBaseFilename)
        'Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for IsCached</summary>
    <TestMethod(), DeploymentItem("D4EM.Data.Source.USGS_Seamless.dll")> Public Sub IsCachedTest()
        Dim lFilesNames As New System.Collections.Specialized.NameValueCollection
        atcUtility.AddFilesInDir(lFilesNames, gCacheFolder & "NLCD", False, "*.zip")
        If lFilesNames.Count > 0 Then
            Dim lFileName As String = lFilesNames.Item(0)
            Assert.IsTrue(USGS_Seamless_Accessor.IsCached(lFileName))
            Dim lCoords() As String = IO.Path.GetFileNameWithoutExtension(lFileName).Split("_")
            Dim lNorth As Double = Double.Parse(lCoords(2))
            Dim lSouth As Double = Double.Parse(lCoords(3))
            Dim lEast As Double = Double.Parse(lCoords(4))
            Dim lWest As Double = Double.Parse(lCoords(5))
            'try looking for a box half a degree inside what's available
            Assert.IsTrue(USGS_Seamless_Accessor.IsCached(gCacheFolder & "NLCD\" & lCoords(0) & "_" & lCoords(1) & "_" & lNorth - 0.5 & "_" & lSouth + 0.5 & "_" & lEast - 0.5 & "_" & lWest + 0.5 & ".zip"))
            'try looking for a box just outside what's available
            Assert.IsTrue(USGS_Seamless_Accessor.IsCached(gCacheFolder & "NLCD\" & lCoords(0) & "_" & lCoords(1) & "_" & lNorth + 0.0001 & "_" & lSouth - 0.0001 & "_" & lEast + 0.0001 & "_" & lWest - 0.0001 & ".zip"))
            'try looking for a box well outside (on the north side) what's available
            Assert.IsFalse(USGS_Seamless_Accessor.IsCached(gCacheFolder & "NLCD\" & lCoords(0) & "_" & lCoords(1) & "_" & lNorth + 0.5 & "_" & lSouth & "_" & lEast & "_" & lWest & ".zip"))
            'try looking for a box well outside (on the south side) what's available
            Assert.IsFalse(USGS_Seamless_Accessor.IsCached(gCacheFolder & "NLCD\" & lCoords(0) & "_" & lCoords(1) & "_" & lNorth & "_" & lSouth - 0.5 & "_" & lEast & "_" & lWest & ".zip"))
            'try looking for a box well outside (on the east side) what's available
            Assert.IsFalse(USGS_Seamless_Accessor.IsCached(gCacheFolder & "NLCD\" & lCoords(0) & "_" & lCoords(1) & "_" & lNorth & "_" & lSouth & "_" & lEast + 0.5 & "_" & lWest & ".zip"))
            'try looking for a box well outside (on the west side) what's available
            Assert.IsFalse(USGS_Seamless_Accessor.IsCached(gCacheFolder & "NLCD\" & lCoords(0) & "_" & lCoords(1) & "_" & lNorth & "_" & lSouth & "_" & lEast & "_" & lWest - 0.5 & ".zip"))
        Else
            Assert.Inconclusive("No Cached File to check.")
        End If
    End Sub

    '''<summary>Test for InputValueToDouble</summary>
    <TestMethod(), DeploymentItem("D4EM.Data.Source.USGS_Seamless.dll")> Public Sub InputValueToDoubleTest()
        Dim aFormElements() As String = Nothing ' TODO: Initialize to an appropriate value
        Dim aName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aDefault As Double = 0.0! ' TODO: Initialize to an appropriate value
        Dim expected As Double = 0.0! ' TODO: Initialize to an appropriate value
        Dim actual As Double
        actual = USGS_Seamless_Accessor.InputValueToDouble(aFormElements, aName, aDefault)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for InputValue</summary>
    <TestMethod(), DeploymentItem("D4EM.Data.Source.USGS_Seamless.dll")> Public Sub InputValueTest()
        Dim aFormElements() As String = Nothing ' TODO: Initialize to an appropriate value
        Dim aName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = USGS_Seamless_Accessor.InputValue(aFormElements, aName)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for GetNodeText</summary>
    <TestMethod(), DeploymentItem("D4EM.Data.Source.USGS_Seamless.dll")> Public Sub GetNodeTextTest()
        Dim target As USGS_Seamless_Accessor = New USGS_Seamless_Accessor() ' TODO: Initialize to an appropriate value
        Dim aDoc As XmlDocument = Nothing ' TODO: Initialize to an appropriate value
        Dim aQuery As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aDefaultText As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = target.GetNodeText(aDoc, aQuery, aDefaultText)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for Execute</summary>
    <TestMethod()> Public Sub ExecuteTest()
        Dim lUSGS_Seamless As USGS_Seamless = New USGS_Seamless()
        Dim lRegion As Region = New Region(Region.RegionTypes.huc8, gHuc8)
        Dim lQuery As New Xml.XmlDocument
        'TODO: put the region in the query along with other needs - see basins
        Dim lResult As String = lUSGS_Seamless.Execute(lQuery.OuterXml)
        Assert.IsTrue(lResult.StartsWith("<success"))
    End Sub

    '''<summary>Test for USGS_Seamless Constructor</summary>
    <TestMethod()> Public Sub USGS_SeamlessConstructorTest()
        Dim lUSGS_Seamless As USGS_Seamless = New USGS_Seamless()
        Assert.AreEqual("D4EM Data Download::USGS_Seamless", lUSGS_Seamless.Name)
    End Sub

    '''<summary>Test for BuildURL</summary>
    <TestMethod(), DeploymentItem("D4EM.Data.Source.USGS_Seamless.dll")> Public Sub BuildURLTest()
        Dim aNorth As Double = 33.0
        Dim aSouth As Double = 32.0
        Dim aWest As Double = -85.0
        Dim aEast As Double = -84.0
        Dim expected As String = "http://extract.cr.usgs.gov/Website/distreq/RequestSummary.jsp?AL=33,32,-84,-85&PL=L0102XZ"
        Dim actual As String = USGS_Seamless_Accessor.BuildURL(1, USGS_Seamless.LayerSpecifications.NLCD2001.LandCover, aNorth, aSouth, aWest, aEast)
        Assert.AreEqual(expected, actual)
        expected = "http://extract.cr.usgs.gov/diststatus/servlet/gov.usgs.edc.RequestStatus?siz=1&key=LCY&ras=1&rsp=0&pfm=GeoTIFF&imsurl=-1&ms=-1&att=-1&lay=-1&fid=-1&dlpre=LCY_&lft=-85&rgt=-84&top=33&bot=32&wmd=1&mur=http://extract.cr.usgs.gov/distmeta/servlet/gov.usgs.edc.MetaBuilder&mcd=NLCD01CANO&mdf=XML&arc=ZIP&sde=NLCD.NLCD_2001_CANOPY&msd=&zun=&prj=0&csx=30.0&csy=30.0&bnd=&bndnm=&RC="
        actual = USGS_Seamless_Accessor.BuildURL(2, USGS_Seamless.LayerSpecifications.NLCD2001.Canopy, aNorth, aSouth, aWest, aEast)
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>Test for Execute</summary>
    <TestMethod()> Public Sub ExecuteTest1()
        Dim lProject As Project = CreateTestProject()
        Dim lSubFolder As String = "NLCD"
        Dim lResult As Object = USGS_Seamless.Execute(lProject, lSubFolder, USGS_Seamless.LayerSpecifications.NLCD2001.LandCover)
        Assert.IsNotNull(lResult)
      End Sub

    '''<summary>Test for Execute</summary>
    <TestMethod(), DeploymentItem("D4EM.Data.Source.USGS_Seamless.dll")> Public Sub ExecuteTest2()
        Dim lProject As Project = CreateTestProject()
        Dim lRegion As Region = lProject.Region
        Dim lCacheFolder As String = lProject.CacheFolder
        Dim lDefaultString As String = "NLCD"
        Dim lDataType As LayerSpecification = USGS_Seamless.LayerSpecifications.NLCD2001.LandCover
        Dim lDesiredProjection As ProjectionInfo = lProject.DesiredProjection
        Dim lCacheOnly As Boolean = False
        Dim lResult As String = USGS_Seamless_Accessor.Execute(lProject, lDefaultString, lDataType)
        Assert.IsTrue(lResult.StartsWith("<success>"))
        'TODO: what about 'layer already exists'?
    End Sub
End Class
