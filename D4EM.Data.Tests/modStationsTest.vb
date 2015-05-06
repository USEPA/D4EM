Imports System.Xml
Imports atcUtility
Imports D4EM.Data
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports D4EM.Data.Source

'''<summary>
'''This is a test class for modStationsTest and is intended
'''to contain all modStationsTest Unit Tests
'''</summary>
<TestClass()> Public Class modStationsTest
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

    '''<summary>Test for MakeStationShapefile</summary>
    <TestMethod()> Public Sub MakeStationShapefileTest()
        Dim aProject As Project = Nothing ' TODO: Initialize to an appropriate value
        Dim aStationDataType As LayerSpecification = Nothing ' TODO: Initialize to an appropriate value
        Dim aAllStations As atcTableRDB = Nothing ' TODO: Initialize to an appropriate value
        Dim aSaveAs As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = NWIS.MakeStationShapefile(aProject, aStationDataType, aAllStations, aSaveAs)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for GetStationsWithWQParameter</summary>
    <TestMethod(), DeploymentItem("D4EM.Data.Source.NWIS.dll")> Public Sub GetStationsWithWQParameterTest()
        Dim aNWLat As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aNWLong As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aSELat As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aSELong As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aStartDate As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aEndDate As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aParameterCode As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim MinCount As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = NWIS_Accessor.GetStationsWithWQParameter(aNWLat, aNWLong, aSELat, aSELong, aStartDate, aEndDate, aParameterCode, MinCount)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for GetStationsInRegion</summary>
    <TestMethod()> Public Sub GetStationsInRegionTest()
        Dim aRegion As Region = Nothing ' TODO: Initialize to an appropriate value
        Dim aSaveAs As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Boolean = False ' TODO: Initialize to an appropriate value
        Dim actual As Boolean
        actual = NWIS.GetStationsInRegion(aRegion, aSaveAs, NWIS.LayerSpecifications.Discharge)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for GetStations</summary>
    <TestMethod()> Public Sub GetStationsTest()
        Dim aArgs As XmlNode = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = NWIS.GetStations(aArgs)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for GetAndMakeShape</summary>
    <TestMethod(), DeploymentItem("D4EM.Data.Source.NWIS.dll")> Public Sub GetAndMakeShapeTest()
        Dim aProject As Project = Nothing ' TODO: Initialize to an appropriate value
        Dim aStationDataType As LayerSpecification = Nothing ' TODO: Initialize to an appropriate value
        Dim aSaveAsBaseFilename As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aMakeShape As Boolean = False ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = NWIS_Accessor.GetAndMakeShape(aProject, aStationDataType, aSaveAsBaseFilename, aMakeShape)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub
End Class
