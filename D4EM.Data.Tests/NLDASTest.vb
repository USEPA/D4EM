Imports DotSpatial.Data
Imports D4EM.Data
Imports System.Xml
Imports DotSpatial.Projections
Imports System.Collections.Generic
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports D4EM.Data.Source

'''<summary>
'''This is a test class for NLDASTest and is intended
'''to contain all NLDASTest Unit Tests
'''</summary>
<TestClass()> _
Public Class NLDASTest
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
        Dim lNLDAS As NLDAS = New NLDAS()
        Dim lQuerySchema As String = lNLDAS.QuerySchema
        Assert.IsTrue(lQuerySchema.Contains("<DataExtension name='NLDASDataExtension'>"))
        Assert.IsTrue(lQuerySchema.Contains("<function name='GetNLDASParameter'"))
    End Sub

    '''<summary>Test for Name</summary>
    <TestMethod()> Public Sub NameTest()
        Dim lNLDAS As NLDAS = New NLDAS()
        Assert.AreEqual("D4EM Data Download::NLDAS", lNLDAS.Name)
    End Sub

    '''<summary>Test for Description</summary>
    <TestMethod()> Public Sub DescriptionTest()
        Dim lNLDAS As NLDAS = New NLDAS()
        Assert.AreEqual("Retrieve BASINS data", lNLDAS.Description)
    End Sub

    '''<summary>Test for NumTimeSteps</summary>
    <TestMethod(), DeploymentItem("D4EM.Data.Source.NLDAS.dll")> Public Sub NumTimeStepsTest()
        Dim target As NLDAS_Accessor = New NLDAS_Accessor() ' TODO: Initialize to an appropriate value
        Dim aDDS As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = target.NumTimeSteps(aDDS)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for MakeStationShapefile</summary>
    <TestMethod()> Public Sub MakeStationShapefileTest()
        'Dim aSaveAs As String = String.Empty ' TODO: Initialize to an appropriate value
        'Dim aPoints As Boolean = False ' TODO: Initialize to an appropriate value
        'Dim aAllCells As List(Of NLDAS.NLDASGridCoords) = Nothing ' TODO: Initialize to an appropriate value
        'Dim aDesiredProjection As ProjectionInfo = Nothing ' TODO: Initialize to an appropriate value
        'Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        'Dim actual As String
        'actual = NLDAS.MakeStationShapefile(aSaveAs, aPoints, aAllCells, aDesiredProjection)
        'Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for GetParameter</summary>
    <TestMethod()> Public Sub GetParameterTest()
        Dim target As NLDAS = New NLDAS() ' TODO: Initialize to an appropriate value
        Dim aArgs As XmlNode = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = target.GetParameter(aArgs)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for GetLocations</summary>
    <TestMethod()> Public Sub GetLocationsTest()
        Dim aProject As Project = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Object = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As Object
        actual = NLDAS.GetLocations(aProject, "NLDAS")
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for GetGridCellsInRegion</summary>
    <TestMethod()> Public Sub GetGridCellsInRegionTest()
        Dim aRegion As Region = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As List(Of NLDAS.NLDASGridCoords) = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As List(Of NLDAS.NLDASGridCoords)
        actual = NLDAS.GetGridCellsInRegion(aRegion)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for GetGridCells</summary>
    <TestMethod()> Public Sub GetGridCellsTest()
        Dim aArgs As XmlNode = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = NLDAS.GetGridCells(aArgs)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for GDS_URL</summary>
    <TestMethod(), DeploymentItem("D4EM.Data.Source.NLDAS.dll")> Public Sub GDS_URLTest()
        Dim target As NLDAS_Accessor = New NLDAS_Accessor() ' TODO: Initialize to an appropriate value
        Dim lBaseURL As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim lDataType As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim lFirstHour As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim lLastHour As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim lCell As NLDAS.NLDASGridCoords = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = target.GDS_URL(lBaseURL, lDataType, lFirstHour, lLastHour, lCell)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for Execute</summary>
    <TestMethod()> Public Sub ExecuteTest()
        Dim lNLDAS As NLDAS = New NLDAS()
        Dim lQuery As String = String.Empty
        Dim lResult = lNLDAS.Execute(lQuery)
        Assert.AreEqual("<error>Root element is missing.</error>", lResult)
        'TODO: add more tests with a query string
      End Sub

    '''<summary>Test for CreateGridSquare</summary>
    <TestMethod(), DeploymentItem("D4EM.Data.Source.NLDAS.dll")> Public Sub CreateGridSquareTest()
        'Dim aGrid As NLDAS.NLDASGridCoords = Nothing ' TODO: Initialize to an appropriate value
        'Dim aTargetProjection As ProjectionInfo = Nothing ' TODO: Initialize to an appropriate value
        'Dim expected As Shape = Nothing ' TODO: Initialize to an appropriate value
        'Dim actual As Shape
        'actual = NLDAS_Accessor.CreateGridSquare(aGrid, aTargetProjection)
        'Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for NLDAS Constructor</summary>
    <TestMethod()> Public Sub NLDASConstructorTest()
        Dim lNLDAS As NLDAS = New NLDAS()
        Assert.AreEqual("D4EM Data Download::NLDAS", lNLDAS.Name)
    End Sub
End Class
