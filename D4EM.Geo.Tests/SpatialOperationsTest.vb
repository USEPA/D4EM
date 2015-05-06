Imports DotSpatial.Projections
Imports System.Collections
Imports System.Collections.Generic
Imports D4EM.Data
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports D4EM.Geo

'''<summary>
'''This is a test class for SpatialOperationsTest and is intended
'''to contain all SpatialOperationsTest Unit Tests
'''</summary>
<TestClass()> Public Class SpatialOperationsTest
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

    '''<summary>Test for SpatialOperations Constructor</summary>
    <TestMethod()> Public Sub SpatialOperationsConstructorTest()
        Dim target As SpatialOperations = New SpatialOperations()
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub

    '''<summary>Test for ChangeGridFormat</summary>
    <TestMethod()> Public Sub ChangeGridFormatTest()
        Dim aSourceFilename As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aDestinationFilename As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = SpatialOperations.ChangeGridFormat(aSourceFilename, aDestinationFilename)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for CopyMoveMergeFile</summary>
    <TestMethod()> Public Sub CopyMoveMergeFileTest()
        Dim aFromFilename As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aDestinationFilename As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aKeyField As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = SpatialOperations.CopyMoveMergeFile(aFromFilename, aDestinationFilename, aKeyField)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for HUC8List</summary>
    <TestMethod()> Public Sub HUC8ListTest()
        Dim aHUC As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As List(Of String) = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As List(Of String)
        actual = SpatialOperations.HUC8List(aHUC)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for MergeLayers</summary>
    <TestMethod()> Public Sub MergeLayersTest()
        Dim aLayers As List(Of Layer) = Nothing ' TODO: Initialize to an appropriate value
        Dim aFromFolder As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aDestinationFolder As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aShapeKeys As Hashtable = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = SpatialOperations.MergeLayers(aLayers, aFromFolder, aDestinationFolder, aShapeKeys)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for ProjectAndClipGridLayers</summary>
    <TestMethod()> Public Sub ProjectAndClipGridLayersTest()
        Dim aFolder As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aNativeProjection As ProjectionInfo = Nothing ' TODO: Initialize to an appropriate value
        Dim aDesiredProjection As ProjectionInfo = Nothing ' TODO: Initialize to an appropriate value
        Dim aClipRegion As Region = Nothing ' TODO: Initialize to an appropriate value
        SpatialOperations.ProjectAndClipGridLayers(aFolder, aNativeProjection, aDesiredProjection, aClipRegion)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>Test for ProjectAndClipGridLayers</summary>
    <TestMethod()> Public Sub ProjectAndClipGridLayersTest1()
        Dim aFolder As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aNativeProjection As ProjectionInfo = Nothing ' TODO: Initialize to an appropriate value
        Dim aDesiredProjection As ProjectionInfo = Nothing ' TODO: Initialize to an appropriate value
        Dim aClipRegion As Region = Nothing ' TODO: Initialize to an appropriate value
        Dim aFilter As String = String.Empty ' TODO: Initialize to an appropriate value
        SpatialOperations.ProjectAndClipGridLayers(aFolder, aNativeProjection, aDesiredProjection, aClipRegion, aFilter)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>Test for ProjectAndClipShapeLayer</summary>
    <TestMethod()> Public Sub ProjectAndClipShapeLayerTest()
        'Dim aShapeFilename As String = String.Empty ' TODO: Initialize to an appropriate value
        'Dim aNativeProjection As ProjectionInfo = Nothing ' TODO: Initialize to an appropriate value
        'Dim aDesiredProjection As ProjectionInfo = Nothing ' TODO: Initialize to an appropriate value
        'Dim aClipRegion As Region = Nothing ' TODO: Initialize to an appropriate value
        'Dim aClipFolder As String = String.Empty ' TODO: Initialize to an appropriate value
        'SpatialOperations.ProjectAndClipShapeLayer(aShapeFilename, aNativeProjection, aDesiredProjection, aClipRegion, aClipFolder)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>Test for ProjectAndClipShapeLayers</summary>
    <TestMethod()> _
    Public Sub ProjectAndClipShapeLayersTest()
        'Dim aFolder As String = String.Empty ' TODO: Initialize to an appropriate value
        'Dim aNativeProjection As ProjectionInfo = Nothing ' TODO: Initialize to an appropriate value
        'Dim aDesiredProjection As ProjectionInfo = Nothing ' TODO: Initialize to an appropriate value
        'Dim aClipRegion As Region = Nothing ' TODO: Initialize to an appropriate value
        'SpatialOperations.ProjectAndClipShapeLayers(aFolder, aNativeProjection, aDesiredProjection, aClipRegion)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>Test for ProjectGrid</summary>
    <TestMethod()> Public Sub ProjectGridTest()
        Dim aNativeProjection As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aDesiredProjection As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aLayerFilename As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aProjectedFilename As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aTrimResult As Boolean = False ' TODO: Initialize to an appropriate value
        Dim aIncrementProgressAfter As Boolean = False ' TODO: Initialize to an appropriate value
        Dim aProgressSameLevel As Boolean = False ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = SpatialOperations.ProjectGrid(aNativeProjection, aDesiredProjection, aLayerFilename, aProjectedFilename, aTrimResult, aIncrementProgressAfter, aProgressSameLevel)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for ProjectImage</summary>
    <TestMethod()> Public Sub ProjectImageTest()
        Dim aCurrentProjection As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aDesiredProjection As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aSourceFilename As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aDestinationFilename As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aIncrementProgressAfter As Boolean = False ' TODO: Initialize to an appropriate value
        Dim aProgressSameLevel As Boolean = False ' TODO: Initialize to an appropriate value
        SpatialOperations.ProjectImage(aCurrentProjection, aDesiredProjection, aSourceFilename, aDestinationFilename, aIncrementProgressAfter, aProgressSameLevel)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>Test for ProjectShapefile</summary>
    <TestMethod()> Public Sub ProjectShapefileTest()
        'Dim aNativeProjection As ProjectionInfo = Nothing ' TODO: Initialize to an appropriate value
        'Dim aDesiredProjection As ProjectionInfo = Nothing ' TODO: Initialize to an appropriate value
        'Dim aShapeFilename As String = String.Empty ' TODO: Initialize to an appropriate value
        'Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        'Dim actual As Boolean
        'actual = SpatialOperations.ProjectShapefile(aNativeProjection, aDesiredProjection, aShapeFilename)
        'Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

End Class
