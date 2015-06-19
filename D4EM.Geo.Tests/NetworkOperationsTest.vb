Imports System.Collections.Generic
Imports DotSpatial.Data
Imports System.IO
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports D4EM.Geo

'''<summary>
'''This is a test class for NetworkOperationsTest and is intended
'''to contain all NetworkOperationsTest Unit Tests
'''</summary>
<TestClass()> Public Class NetworkOperationsTest

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
            testContextInstance = value
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

    '''<summary>Test for NetworkOperations Constructor</summary>
    <TestMethod()> Public Sub NetworkOperationsConstructorTest()
        Dim target As NetworkOperations = New NetworkOperations()
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub

    '''<summary>Test for CheckConnectivity</summary>
    <TestMethod(), DeploymentItem("D4EM.Geo.dll")> Public Sub CheckConnectivityTest()
        Dim aFlowlinesShapeFile As FeatureSet = Nothing ' TODO: Initialize to an appropriate value
        Dim aFields As NetworkOperations.FieldIndexes = Nothing ' TODO: Initialize to an appropriate value
        Dim aOutletComIDs As List(Of Long) = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Generic.List(Of Long) = Nothing ' TODO: Initialize to an appropriate value
        Dim actual = NetworkOperations.CheckConnectivity(aFlowlinesShapeFile, aFields)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for ClipFlowLinesToCatchments</summary>
    <TestMethod()> Public Sub ClipFlowLinesToCatchmentsTest()
        'Dim aCatchmentsFilename As String = String.Empty ' TODO: Initialize to an appropriate value
        'Dim aFlowLinesShapeFilename As String = String.Empty ' TODO: Initialize to an appropriate value
        'Dim aNewFlowlinesFilename As String = String.Empty ' TODO: Initialize to an appropriate value
        'Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        'Dim actual As Boolean
        'actual = NetworkOperations.ClipFlowLinesToCatchments(aCatchmentsFilename, aFlowLinesShapeFilename, aNewFlowlinesFilename)
        'Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for CombineCatchments</summary>
    <TestMethod(), DeploymentItem("D4EM.Geo.dll")> _
    Public Sub CombineCatchmentsTest()
        Dim aCatchmentShapeFile As FeatureSet = Nothing ' TODO: Initialize to an appropriate value
        Dim aKeptComId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim aAddingComId As Long = 0 ' TODO: Initialize to an appropriate value
        Dim aFields As NetworkOperations.FieldIndexes = Nothing ' TODO: Initialize to an appropriate value
        Dim actual = NetworkOperations.CombineCatchments(aCatchmentShapeFile, aKeptComId, aAddingComId, aFields)
        Assert.IsTrue(actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for CombineFlowlines</summary>
    <TestMethod(), DeploymentItem("D4EM.Geo.dll")> Public Sub CombineFlowlinesTest()
        Dim aFlowlinesShapeFile As FeatureSet = Nothing ' TODO: Initialize to an appropriate value
        Dim aSourceBaseIndex As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim aSourceDeletingIndex As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim aMergeShapes As Boolean = False ' TODO: Initialize to an appropriate value
        Dim aKeepCosmeticRemovedLine As Boolean = False ' TODO: Initialize to an appropriate value
        Dim aFields As NetworkOperations.FieldIndexes = Nothing ' TODO: Initialize to an appropriate value
        Dim aOutletComIDs As List(Of Long) = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = NetworkOperations.CombineFlowlines(aFlowlinesShapeFile, aSourceBaseIndex, aSourceDeletingIndex, aMergeShapes, aKeepCosmeticRemovedLine, aFields, aOutletComIDs)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for CopyAndOpenNewShapefile</summary>
    <TestMethod(), DeploymentItem("D4EM.Geo.dll")> Public Sub CopyAndOpenNewShapefileTest()
        'Dim aOldFilename As String = String.Empty ' TODO: Initialize to an appropriate value
        'Dim aNewFilename As String = String.Empty ' TODO: Initialize to an appropriate value
        'Dim expected As FeatureSet = Nothing ' TODO: Initialize to an appropriate value
        'Dim actual As FeatureSet = NetworkOperations_Accessor.CopyAndOpenNewShapefile(aOldFilename, aNewFilename)
        'Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for Count</summary>
    <TestMethod(), DeploymentItem("D4EM.Geo.dll")> Public Sub CountTest()
        'Dim aShapeFile As FeatureSet = Nothing ' TODO: Initialize to an appropriate value
        'Dim aFieldIndex As Integer = 0 ' TODO: Initialize to an appropriate value
        'Dim aFieldValue As Object = Nothing ' TODO: Initialize to an appropriate value
        'Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        'Dim actual As Integer
        'actual = NetworkOperations_Accessor.Count(aShapeFile, aFieldIndex, aFieldValue)
        'Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for DumpComid</summary>
    <TestMethod(), DeploymentItem("D4EM.Geo.dll")> Public Sub DumpComidTest()
        'Dim aFlowLinesShapeFile As FeatureSet = Nothing ' TODO: Initialize to an appropriate value
        'Dim aComId As Long = 0 ' TODO: Initialize to an appropriate value
        'Dim aFields As NetworkOperations.FieldIndexes = Nothing ' TODO: Initialize to an appropriate value
        'Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        'Dim actual As String
        'actual = NetworkOperations_Accessor.DumpComid(aFlowLinesShapeFile, aComId, aFields)
        'Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for FieldIndex</summary>
    <TestMethod(), DeploymentItem("D4EM.Geo.dll")> Public Sub FieldIndexTest()
        'Dim aShapeFile As FeatureSet = Nothing ' TODO: Initialize to an appropriate value
        'Dim aFieldName As String = String.Empty ' TODO: Initialize to an appropriate value
        'Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        'Dim actual As Integer
        'actual = NetworkOperations_Accessor.FieldIndexes.FieldIndex(aShapeFile, aFieldName)
        'Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for FindRecord</summary>
    <TestMethod(), DeploymentItem("D4EM.Geo.dll")> Public Sub FindRecordTest()
        'Dim aShapeFile As FeatureSet = Nothing ' TODO: Initialize to an appropriate value
        'Dim aFieldIndex As Integer = 0 ' TODO: Initialize to an appropriate value
        'Dim aValue As Long = 0 ' TODO: Initialize to an appropriate value
        'Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        'Dim actual As Integer
        'actual = NetworkOperations_Accessor.FindRecord(aShapeFile, aFieldIndex, aValue)
        'Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for FindRecords</summary>
    <TestMethod(), DeploymentItem("D4EM.Geo.dll")> Public Sub FindRecordsTest()
        'Dim aShapeFile As FeatureSet = Nothing ' TODO: Initialize to an appropriate value
        'Dim aFieldIndex As Integer = 0 ' TODO: Initialize to an appropriate value
        'Dim aValue As Long = 0 ' TODO: Initialize to an appropriate value
        'Dim expected As List(Of Integer) = Nothing ' TODO: Initialize to an appropriate value
        'Dim actual As List(Of Integer)
        'actual = NetworkOperations_Accessor.FindRecords(aShapeFile, aFieldIndex, aValue)
        'Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for ReconnectUpstreamToDownstream</summary>
    <TestMethod(), DeploymentItem("D4EM.Geo.dll")> Public Sub ReconnectUpstreamToDownstreamTest()
        'Dim aFlowlinesShapeFile As FeatureSet = Nothing ' TODO: Initialize to an appropriate value
        'Dim aDeletedComId As Long = 0 ' TODO: Initialize to an appropriate value
        'Dim aDownstreamComId As Long = 0 ' TODO: Initialize to an appropriate value
        'Dim aFields As NetworkOperations.FieldIndexes = Nothing ' TODO: Initialize to an appropriate value
        'NetworkOperations_Accessor.ReconnectUpstreamToDownstream(aFlowlinesShapeFile, aDeletedComId, aDownstreamComId, aFields)
        Assert.Inconclusive("A method that does not return a value cannot be verified.")
    End Sub

    '''<summary>Test for RemoveBraidedFlowlines</summary>
    <TestMethod()> Public Sub RemoveBraidedFlowlinesTest()
        'Dim aFlowlinesFileName As String = String.Empty ' TODO: Initialize to an appropriate value
        'Dim aCatchmentFileName As String = String.Empty ' TODO: Initialize to an appropriate value
        'Dim aNewFlowlinesFilename As String = String.Empty ' TODO: Initialize to an appropriate value
        'Dim aNewCatchmentFilename As String = String.Empty ' TODO: Initialize to an appropriate value
        'Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        'Dim actual As Boolean
        'actual = NetworkOperations.RemoveBraidedFlowlines(aFlowlinesFileName, aCatchmentFileName, aNewFlowlinesFilename, aNewCatchmentFilename)
        'Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

End Class
