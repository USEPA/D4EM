Imports System.Collections
Imports System
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports D4EM.Data.Source

'''<summary>
'''This is a test class for NWISTest and is intended
'''to contain all NWISTest Unit Tests
'''</summary>
<TestClass()> Public Class NWISTest
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

    '''<summary>Test for QuerySchema</summary>
    <TestMethod()> Public Sub QuerySchemaTest()
        Dim lNWIS As NWIS = New NWIS()
        Dim lQuerySchema As String = lNWIS.QuerySchema
        Assert.IsTrue(lQuerySchema.Contains("<DataExtension name='NWISDataExtension'>"))
        Assert.IsTrue(lQuerySchema.Contains("<function name='GetNWISDailyDischarge'"))
    End Sub

    '''<summary>Test for Name</summary>
    <TestMethod()> Public Sub NameTest()
        Dim lNWIS As NWIS = New NWIS()
        Assert.AreEqual("D4EM Data Download::NWIS", lNWIS.Name)
    End Sub

    '''<summary>Test for Description</summary>
    <TestMethod()> Public Sub DescriptionTest()
        Dim lNWIS As NWIS = New NWIS()
        Assert.AreEqual("Retrieve NWIS station information and data", lNWIS.Description)
    End Sub

    '''<summary>Test for getDischargeValues</summary>
    <TestMethod(), DeploymentItem("D4EM.Data.Source.NWIS.dll")> Public Sub getDischargeValuesTest()
        Dim lNWIS_Accessor As NWIS_Accessor = New NWIS_Accessor()
        Dim lStationNumber As String = "02336300"
        Dim lStartDate As String = "2009/01/01"
        Dim lEndDate As String = "2010/12/31"
        Dim lActual As String = lNWIS_Accessor.getDischargeValues(lStationNumber, lStartDate, lEndDate)
        Assert.IsTrue(lActual.Contains("Data provided for site " & lStationNumber))
        Assert.IsTrue(lActual.Contains("A" & vbLf & "USGS" & vbTab & "02336300" & vbTab & "2009"))
       End Sub

    '''<summary>Test for StationTypeName</summary>
    <TestMethod(), DeploymentItem("D4EM.Data.Source.NWIS.dll")> Public Sub StationTypeNameTest()
        Dim lStationType As NWIS.StationType = NWIS.StationType.LakeOrReservoir
        Assert.AreEqual("LakeOrReservoir", NWIS_Accessor.StationTypeName(lStationType))
        lStationType = NWIS.StationType.Estuary
        Assert.AreEqual("Estuary", NWIS_Accessor.StationTypeName(lStationType))
        lStationType = NWIS.StationType.Groundwater
        Assert.AreEqual("Groundwater", NWIS_Accessor.StationTypeName(lStationType))
        lStationType = NWIS.StationType.Meteorological
        Assert.AreEqual("Meteorological", NWIS_Accessor.StationTypeName(lStationType))
        lStationType = NWIS.StationType.Spring
        Assert.AreEqual("Spring", NWIS_Accessor.StationTypeName(lStationType))
        lStationType = NWIS.StationType.StreamOrRiver
        Assert.AreEqual("StreamOrRiver", NWIS_Accessor.StationTypeName(lStationType))
    End Sub

    '''<summary>Test for MakeKMLFileForNeuse</summary>
    <TestMethod(), DeploymentItem("D4EM.Data.Source.NWIS.dll")> Public Sub MakeKMLFileForNeuseTest()
        Dim lNWIS_Accessor As NWIS_Accessor = New NWIS_Accessor()
        Dim lParameterCode As String = "60"
        Dim lResult As String = lNWIS_Accessor.MakeKMLFileForNeuse(lParameterCode)
        Assert.AreEqual("In development", lResult)
     End Sub

    '''<summary>Test for GetWQParamUnits</summary>
    <TestMethod(), DeploymentItem("D4EM.Data.Source.NWIS.dll")> Public Sub GetWQParamUnitsTest()
        Dim lNWIS_Accessor As NWIS_Accessor = New NWIS_Accessor()
        Dim ParamCode As String = "00060"
        Dim lResult As String = lNWIS_Accessor.GetWQParamUnits(ParamCode)
        Assert.AreEqual("CFS", lResult)
      End Sub

    '''<summary>Test for GetValuesFromNWISWebpage</summary>
    <TestMethod(), DeploymentItem("D4EM.Data.Source.NWIS.dll")> Public Sub GetValuesFromNWISWebpageTest()
        Dim target As NWIS_Accessor = New NWIS_Accessor() ' TODO: Initialize to an appropriate value
        Dim url As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim Variable As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim StartDate As DateTime = New DateTime() ' TODO: Initialize to an appropriate value
        Dim EndDate As DateTime = New DateTime() ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = target.GetValuesFromNWISWebpage(url, Variable, StartDate, EndDate)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for GetStationsWithWQParameter</summary>
    <TestMethod(), DeploymentItem("D4EM.Data.Source.NWIS.dll")> Public Sub GetStationsWithWQParameterTest()
        Dim target As NWIS_Accessor = New NWIS_Accessor() ' TODO: Initialize to an appropriate value
        Dim HUC As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim ParameterCode As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim StationCode As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As ArrayList
        actual = target.GetStationsWithWQParameter(HUC, ParameterCode, StationCode)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for GetSiteInfo</summary>
    <TestMethod(), DeploymentItem("D4EM.Data.Source.NWIS.dll")> Public Sub GetSiteInfoTest()
        Dim target As NWIS_Accessor = New NWIS_Accessor() ' TODO: Initialize to an appropriate value
        Dim SiteCode As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = target.GetSiteInfo(SiteCode)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for GetParameterInfo</summary>
    <TestMethod(), DeploymentItem("D4EM.Data.Source.NWIS.dll")> Public Sub GetParameterInfoTest()
        Dim target As NWIS_Accessor = New NWIS_Accessor() ' TODO: Initialize to an appropriate value
        Dim ParameterCode As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = target.GetParameterInfo(ParameterCode)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for GetNWISStatsEmptyXML</summary>
    <TestMethod(), DeploymentItem("D4EM.Data.Source.NWIS.dll")> Public Sub GetNWISStatsEmptyXMLTest()
        Dim target As NWIS_Accessor = New NWIS_Accessor() ' TODO: Initialize to an appropriate value
        Dim HUC As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = target.GetNWISStatsEmptyXML(HUC)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for GetNWISStatistics</summary>
    <TestMethod()> Public Sub GetNWISStatisticsTest()
        Dim target As NWIS = New NWIS() ' TODO: Initialize to an appropriate value
        Dim aHUC As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aParameterCode As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aStationType As NWIS.StationType = New NWIS.StationType() ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = target.GetNWISStatistics(aHUC, aParameterCode, aStationType)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for Execute</summary>
    <TestMethod()> Public Sub ExecuteTest()
        Dim lNWIS As NWIS = New NWIS()
        Dim lQuery As String = String.Empty
        Dim lResult = lNWIS.Execute(lQuery)
        Assert.AreEqual("<error>Root element is missing.</error>", lResult)
        'TODO: add more tests with a query string
    End Sub

    '''<summary>Test for NWIS Constructor</summary>
    <TestMethod()> Public Sub NWISConstructorTest()
        Dim lNWIS As NWIS = New NWIS()
        Assert.AreEqual("D4EM Data Download::NWIS", lNWIS.Name)
    End Sub
End Class
