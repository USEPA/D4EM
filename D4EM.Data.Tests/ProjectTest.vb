Imports DotSpatial.Data
Imports DotSpatial.Projections
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports D4EM.Data

'''<summary>
'''This is a test class for ProjectTest and is intended
'''to contain all ProjectTest Unit Tests
'''</summary>
<TestClass()> _
Public Class ProjectTest
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
    Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
    End Sub
    '
    'Use ClassCleanup to run code after all tests in a class have run
    '<ClassCleanup()>  _
    Public Shared Sub MyClassCleanup()
    End Sub
    '
    'Use TestInitialize to run code before running each test
    '<TestInitialize()>  _
    Public Sub MyTestInitialize()
    End Sub
    '
    'Use TestCleanup to run code after each test has run
    '<TestCleanup()>  _
    Public Sub MyTestCleanup()
    End Sub
    '
#End Region

    '''<summary>Test for Summary</summary>
    <TestMethod()> Public Sub SummaryTest()
        Dim lProject As Project = CreateTestProject()
        Assert.AreEqual(lProject.Summary, "Layers: 0 with 0 FeatureSets 0 PolygonShapeFiles 0 PointShapeFiles, 0 TimeseriesSources")
    End Sub

    '''<summary>Test for LayerFromTag</summary>
    <TestMethod()> Public Sub LayerFromTagTest()
        Dim lProject As Project = CreateTestProject()
        Dim lTag As String = "County"
        Dim lLayer As New D4EM.Data.Layer(lProject.ProjectFolder & "cnty.shp", New D4EM.Data.LayerSpecification("County"))
        lProject.Layers.Add(lLayer)
        Dim lLayerReturned As D4EM.Data.Layer = lProject.LayerFromTag(lTag)
        Assert.AreEqual(lLayer, lLayerReturned)
    End Sub

    '''<summary>Test for GetImageData</summary>
    <TestMethod()> Public Sub GetImageDataTest()
        Dim lProject As Project = CreateTestProject()
        'Dim aTag As String = String.Empty ' TODO: Initialize to an appropriate value
        'Dim expected As ImageData = Nothing ' TODO: Initialize to an appropriate value
        'Dim actual As ImageData
        'actual = lProject.GetImageData(aTag)
        'Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for GetFeatureSet</summary>
    <TestMethod()> Public Sub GetFeatureSetTest()
        Dim lProject As Project = CreateTestProject()
        Dim lTag As String = "County"
        Dim lLayer As New D4EM.Data.Layer(lProject.ProjectFolder & "cnty.shp", New D4EM.Data.LayerSpecification("County"))
        lProject.Layers.Add(lLayer)
        Dim lFeatureSetReturned As FeatureSet = lProject.GetFeatureSet(lTag)
        Assert.AreEqual(lLayer.DataSet, lFeatureSetReturned)
    End Sub

    '''<summary>Test for Project Constructor</summary>
    <TestMethod()> Public Sub ProjectConstructorTest()
        Dim lProject As Project = CreateTestProject()
        Assert.IsNotNull(lProject)
        Assert.AreEqual(lProject.Layers.Count, 0)
        Assert.AreEqual(lProject.TimeseriesSources.Count, 0)
        Assert.AreEqual(lProject.Region.GetKeys(Region.RegionTypes.huc8).Count, 1)
    End Sub
End Class
