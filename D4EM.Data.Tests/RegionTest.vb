Imports System
Imports DotSpatial.Topology
Imports DotSpatial.Data
Imports DotSpatial.Projections
Imports System.Collections.Generic
Imports System.Xml
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports D4EM.Data

'''<summary>
'''This is Test class for RegionTest and is intended
'''to contain all RegionTest Unit Tests
'''</summary>
<TestClass()> _
Public Class RegionTest
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
    Private pHuc8 As String = "03070103"
    Private pDataFolder As String = IO.Directory.GetCurrentDirectory & "\..\..\..\D4EM.Data.Tests\data\" & pHuc8 & "\"
    Private pNorthbc As String = "4035092.52011145"
    Private pNorthbcXML As String = "<northbc>" & pNorthbc & "</northbc>"
    Private pSouthbc As String = "3845518.37252247"
    Private pSouthbcXML As String = "<southbc>" & pSouthbc & "</southbc>"
    Private pEastbc As String = "-9291895.47737651"
    Private pEastbcXML As String = "<eastbc>" & pEastbc & "</eastbc>"
    Private pWestbc As String = "-9400130.23178286"
    Private pWestbcXML As String = "<westbc>" & pWestbc & "</westbc>"
    Private pProjectionStringGoogleMerc = "+proj=merc +lon_0=0 +lat_ts=0 +x_0=0 +y_0=0 +a=6378137 +b=6378137 +units=m +no_defs"
    Private pProjectionXML As String = "<projection>" & pProjectionStringGoogleMerc & "</projection>"
    Private pRegionXML As String = "<region>" & pNorthbcXML & pSouthbcXML & pEastbcXML & pWestbcXML & pProjectionXML &
        "<HUC8 status='set by BASINS System Application'>" & pHuc8 & "</HUC8></region>"
    Private pProjectionStringAlbers As String = "+x_0=0 +y_0=0 +lat_0=23 +lon_0=-96 +lat_1=29.5 +lat_2=45.5 +proj=aea +ellps=GRS80 +no_defs"

    Private Function BuildRegion() As Region
        Dim lXMLDoc As New XmlDocument
        lXMLDoc.LoadXml(pRegionXML)
        Return New Region(lXMLDoc)
    End Function

    '''<summary>Test for PreferredFormat</summary>
    <TestMethod()> Public Sub PreferredFormatTest()
        Dim lRegion As Region = BuildRegion()
        Assert.AreEqual(lRegion.RegionSpecification, Region.RegionTypes.box)
    End Sub

    '''<summary>Test for XML</summary>
    <TestMethod()> Public Sub XMLTest()
        Dim lRegion As Region = BuildRegion()
        Assert.IsTrue(lRegion.XML.Contains(pNorthbcXML))
        Assert.IsTrue(lRegion.XML.Contains(pSouthbcXML))
        Assert.IsTrue(lRegion.XML.Contains(pEastbcXML))
        Assert.IsTrue(lRegion.XML.Contains(pWestbcXML))
        Dim lProjectionInfo As New DotSpatial.Projections.ProjectionInfo(pProjectionStringGoogleMerc)
        Assert.IsTrue(lRegion.XML.Contains(lProjectionInfo.ToProj4String))
        Assert.IsTrue(lRegion.XML.Contains(pHuc8))
    End Sub

    '''<summary>Test for Validate</summary>
    <TestMethod()> Public Sub ValidateTest()
        Dim lRegion As Region = BuildRegion()
        Assert.IsTrue(lRegion.Validate())
    End Sub

    '''<summary>Test for ToString</summary>
    <TestMethod()> Public Sub ToStringTest()
        Dim lRegion As Region = BuildRegion()
        Assert.AreEqual("Region Box North 4035092.52011145 South 3845518.37252247 West -9400130.23178286 East -9291895.47737651", lRegion.ToString)
     End Sub

    '''<summary>Test for ToShape</summary>
    <TestMethod()> Public Sub ToShapeTest()
        Dim lGoogleProjection As New DotSpatial.Projections.ProjectionInfo(pProjectionStringGoogleMerc)
        Dim lRegion As Region = BuildRegion()
        Dim lShape As DotSpatial.Data.Shape = lRegion.ToShape(lGoogleProjection)
        Assert.AreEqual(lShape.Vertices.Count, 8)

        Dim aNewProjection As New ProjectionInfo(pProjectionStringAlbers)
        lShape = lRegion.ToShape(aNewProjection)
        Assert.AreEqual(lShape.Vertices.Count, 8)

        lRegion = New Region(Region.RegionTypes.huc8, pHuc8)
        lShape = lRegion.ToShape(lGoogleProjection)
        Assert.AreEqual(lShape.Vertices.Count, 1012)
     End Sub

    '''<summary>Test for SelectShapes</summary>
    <TestMethod()> Public Sub SelectShapesTest()
        Dim lRegion As Region = BuildRegion()
        Dim lShapeLayer As New D4EM.Data.Layer(pDataFolder & "cnty.shp", D4EM.Data.Source.BASINS.LayerSpecifications.core31.cnty)
        Dim lClippedFilename As String = pDataFolder & "cntyClipped.shp"
        Dim lClippedLayer As D4EM.Data.Layer = lRegion.SelectShapes(lShapeLayer, lClippedFilename)
        Assert.AreEqual(20, lClippedLayer.AsFeatureSet.NumRows)
    End Sub

    ' '''<summary>Test for GetProjected</summary>
    '<TestMethod()> Public Sub GetProjectedTest()
    '    Dim lRegion As Region = BuildRegion()
    '    Dim lRegionActual As Region = lRegion.GetProjected(New ProjectionInfo(pProjectionStringAlbers))
    '    Dim lRegionExpected As New Region(pHuc8)
    '    Assert.AreEqual(lRegionExpected, lRegionActual)
    ' End Sub

    '''<summary>Test for GetKeysOfOverlappingShapes</summary>
    <TestMethod()> Public Sub GetKeysOfOverlappingShapesTest()
        Dim lRegion As Region = BuildRegion()
        Dim lSelectFromShapeFilename As String = pDataFolder & "cnty.shp"
        Dim lKeyFieldName As String = "CNTYNAME"
        Dim lExpected As New List(Of String) From
           {"FULTON", "GWINNETT", "DE KALB", "WALTON", "ROCKDALE", "NEWTON", "CLAYTON",
            "HENRY", "JASPER", "BUTTS", "SPAULDING", "LAMAR", "MONROE", "JONES", "UPSON",
            "BIBB", "TWIGGS", "CRAWFORD", "PEACH", "HOUSTON"}
        Dim lKeys As List(Of String) = lRegion.GetKeysOfOverlappingShapes(lSelectFromShapeFilename, lKeyFieldName)
        Assert.AreEqual(lExpected.Count, lKeys.Count)
     End Sub

    '''<summary>Test for GetKeysOfOverlappingShapes</summary>
    <TestMethod()> Public Sub GetKeysOfOverlappingShapesTest1()
        Dim lRegion As Region = BuildRegion()
        Dim lSelectFromLayer As PolygonShapefile = New PolygonShapefile(pDataFolder & "cnty.shp")
        Dim lKeyField As Integer = 7
        Dim lExpected As New List(Of String) From
           {"FULTON", "GWINNETT", "DE KALB", "WALTON", "ROCKDALE", "NEWTON", "CLAYTON",
            "HENRY", "JASPER", "BUTTS", "SPAULDING", "LAMAR", "MONROE", "JONES", "UPSON",
            "BIBB", "TWIGGS", "CRAWFORD", "PEACH", "HOUSTON"}
        Dim lActual As List(Of String) = lRegion.GetKeysOfOverlappingShapes(lSelectFromLayer, lKeyField)
        Assert.AreEqual(lExpected.Count, lActual.Count)
     End Sub

    '''<summary>Test for GetBounds</summary>
    <TestMethod()> Public Sub GetBoundsTest()
        Dim lRegion As Region = BuildRegion()
        Dim aNorth As Double = 0.0
        Dim aSouth As Double = 0.0
        Dim aWest As Double = 0.0
        Dim aEast As Double = 0.0
        Dim aNewProjection As ProjectionInfo = New ProjectionInfo(pProjectionStringGoogleMerc)
        lRegion.GetBounds(aNorth, aSouth, aWest, aEast, aNewProjection)
        Assert.AreEqual(CDbl(pNorthbc), aNorth, 0.0001)
        Assert.AreEqual(CDbl(pSouthbc), aSouth, 0.0001)
        Assert.AreEqual(CDbl(pWestbc), aWest, 0.0001)
        Assert.AreEqual(CDbl(pEastbc), aEast, 0.0001)
     End Sub

    '''<summary>Test for ClipGrid</summary>
    <TestMethod()> Public Sub ClipGridTest()
        Dim lRegion As Region
        Dim lProjection As ProjectionInfo = New ProjectionInfo(pProjectionStringAlbers)

        Dim lCountyShapefile As PolygonShapefile = New PolygonShapefile(pDataFolder & "cnty.shp")
        Dim lCountyGeometry As DotSpatial.Topology.Geometry = lCountyShapefile.GetShape(5, True).ToGeometry
        With lCountyGeometry.EnvelopeAsGeometry
            lRegion = New Region(.Coordinates(2).Y, .Coordinates(0).Y, .Coordinates(2).X, .Coordinates(0).X, lProjection)
        End With

        Dim lGridFilename As String = pDataFolder & "NLCD_landcover_2001.tif"
        Dim lClippedFilename As String = pDataFolder & "NLCD_landcover_2001_clipped.tif"
        lRegion.ClipGrid(lGridFilename, lClippedFilename, lProjection)
        Assert.IsTrue(IO.File.Exists(lClippedFilename))
    End Sub

    '''<summary>Test for Region Constructor</summary>
    <TestMethod()> Public Sub RegionConstructorTest()
        Dim lProjection As ProjectionInfo = New ProjectionInfo(pProjectionStringAlbers)
        Dim lRegion As Region = New Region(pNorthbc, pSouthbc, pWestbc, pEastbc, lProjection)
        Dim lEast As Double, lWest As Double, lNorth As Double, lSouth As Double
        lRegion.GetBounds(lNorth, lSouth, lWest, lEast, lProjection)
        Assert.AreEqual(lEast, CDbl(pEastbc), 0.0001)
        Assert.AreEqual(lWest, CDbl(pWestbc), 0.0001)
        Assert.AreEqual(lNorth, CDbl(pNorthbc), 0.0001)
        Assert.AreEqual(lSouth, CDbl(pSouthbc), 0.0001)
      End Sub

    '''<summary>Test for Region Constructor</summary>
    <TestMethod()> Public Sub RegionConstructorTest1()
        Dim lXMLDoc As New XmlDocument
        lXMLDoc.LoadXml(pRegionXML)
        Dim lRegionNode As XmlNode = lXMLDoc
        Dim lRegion As Region = New Region(lRegionNode)
        Assert.AreEqual(BuildRegion, lRegion)
    End Sub

    '''<summary>Test for Region Constructor</summary>
    <TestMethod()> Public Sub RegionConstructorTest2()
        Dim lRegionExpected As Region = BuildRegion()
        Dim lRegion As Region = New Region(Region.RegionTypes.huc8, pHuc8)
        Assert.IsFalse(lRegion.Equals(lRegionExpected))

        Dim lEast As Double, lWest As Double, lNorth As Double, lSouth As Double
        Dim lProjection As ProjectionInfo = New ProjectionInfo(pProjectionStringGoogleMerc)
        lRegion.GetBounds(lNorth, lSouth, lWest, lEast, lProjection)
        lRegion = New Region(lNorth, lSouth, lWest, lEast, lProjection)
        Assert.IsTrue(lRegion.Equals(lRegionExpected))
    End Sub

    '''<summary>Test for Region Constructor</summary>
    <TestMethod()> Public Sub RegionConstructorTest3()
        Dim lPolygon As DotSpatial.Data.Shape = Nothing ' TODO: Initialize to an appropriate value
        Dim lProjection As ProjectionInfo = New ProjectionInfo(pProjectionStringAlbers)
        Dim lRegion As Region = New Region(lPolygon, lProjection)
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub

    '''<summary>Test Region Constructor</summary>
    <TestMethod()> Public Sub RegionConstructorTest4()
        Dim target As Region = New Region(Region.RegionTypes.huc8, pHuc8)
        Assert.AreEqual(target.GetKeys(Region.RegionTypes.huc8)(0), pHuc8)
    End Sub

    '''<summary>Test AddKey</summary>
    <TestMethod(), DeploymentItem("D4EM.Data.dll")> _
    Public Sub AddKeyTest()
        Dim param0 As PrivateObject = Nothing ' TODO: Initialize to an appropriate value
        Dim target As Region_Accessor = New Region_Accessor(param0) ' TODO: Initialize to an appropriate value
        target.AddKey(Region.RegionTypes.huc8, pHuc8)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>Test BoundsFromKeys</summary>
    <TestMethod()> _
    Public Sub BoundsFromKeysTest()
        Dim lRegion As PrivateObject = Nothing
        Dim target As Region_Accessor = New Region_Accessor(lRegion) ' TODO: Initialize to an appropriate value
        target.BoundsFromKeys(Region.RegionTypes.huc8)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>Test ClipGrid</summary>
    <TestMethod()> Public Sub ClipGridTest1()
        Dim aArgs As XmlNode = Nothing ' TODO: Initialize to an appropriate value
        Dim target As Region = New Region(aArgs) ' TODO: Initialize to an appropriate value
        Dim aGridFilename As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aClippedFilename As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aGridProjection As ProjectionInfo = Nothing ' TODO: Initialize to an appropriate value
        target.ClipGrid(aGridFilename, aClippedFilename, aGridProjection)
        Assert.IsTrue(IO.File.Exists(aClippedFilename))
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test Equals</summary>
    <TestMethod()> Public Sub EqualsTest()
        Dim aArgs As XmlNode = Nothing ' TODO: Initialize to an appropriate value
        Dim target As Region = New Region(aArgs) ' TODO: Initialize to an appropriate value
        Dim aRegionObject As Object = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = target.Equals(aRegionObject)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test GetExtent</summary>
    <TestMethod()> Public Sub GetExtentTest()
        Dim aArgs As XmlNode = Nothing ' TODO: Initialize to an appropriate value
        Dim target As Region = New Region(aArgs) ' TODO: Initialize to an appropriate value
        Dim aNewProjection As ProjectionInfo = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Extent = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As Extent
        actual = target.GetExtent(aNewProjection)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test GetKeys</summary>
    <TestMethod()> Public Sub GetKeysTest()
        Dim lRegion As Region = BuildRegion()
        Dim lExpected As New List(Of String) From {pHuc8}
        Dim lActual As List(Of String) = lRegion.GetKeys(Region.RegionTypes.huc8)
        Assert.AreEqual(lExpected.Count, lActual.Count)
        Assert.AreEqual(lExpected.Item(0), lActual.Item(0))
        'TODO: add tests for other RegionTypes
    End Sub

    '''<summary>Test RegionSpecificationFromString</summary>
    <TestMethod(), DeploymentItem("D4EM.Data.dll")> _
    Public Sub RegionSpecificationFromStringTest()
        Dim aSpecificationName As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As LayerSpecification = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As LayerSpecification
        actual = Region_Accessor.RegionSpecificationFromString(aSpecificationName)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test SetPrivate</summary>
    <TestMethod(), DeploymentItem("D4EM.Data.dll")> _
    Public Sub SetPrivateTest()
        Dim param0 As PrivateObject = Nothing ' TODO: Initialize to an appropriate value
        Dim target As Region_Accessor = New Region_Accessor(param0) ' TODO: Initialize to an appropriate value
        Dim aPart As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aNewValue As String = String.Empty ' TODO: Initialize to an appropriate value
        target.SetPrivate(aPart, aNewValue)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>Test SetXML</summary>
    <TestMethod(), DeploymentItem("D4EM.Data.dll")> _
    Public Sub SetXMLTest()
        Dim param0 As PrivateObject = Nothing ' TODO: Initialize to an appropriate value
        Dim target As Region_Accessor = New Region_Accessor(param0) ' TODO: Initialize to an appropriate value
        Dim aXML As XmlNode = Nothing ' TODO: Initialize to an appropriate value
        target.SetXML(aXML)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub
 
    '''<summary>Test NationalShapeFilename</summary>
    <TestMethod(), DeploymentItem("D4EM.Data.dll")> _
    Public Sub NationalShapeFilenameTest()
        Dim aKeyType As LayerSpecification = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        Region_Accessor.NationalShapeFilename(aKeyType) = expected
        actual = Region_Accessor.NationalShapeFilename(aKeyType)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test RegionSpecification</summary>
    <TestMethod()> Public Sub RegionSpecificationTest()
        Dim aArgs As XmlNode = Nothing ' TODO: Initialize to an appropriate value
        Dim target As Region = New Region(aArgs) ' TODO: Initialize to an appropriate value
        Dim actual As LayerSpecification
        actual = target.RegionSpecification
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub
End Class
