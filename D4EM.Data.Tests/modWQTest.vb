Imports System.Xml
Imports System.Collections
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports D4EM.Data.Source

'''<summary>
'''This is a test class for modWQTest and is intended
'''to contain all modWQTest Unit Tests
'''</summary>
<TestClass()> Public Class modWQTest
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

    '''<summary>Test for GetWQValues</summary>
    <TestMethod()> Public Sub GetWQValuesTest()
        Dim aStationNumber As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aParamCode As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aStartDate As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aEndDate As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As ArrayList = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As ArrayList
        actual = NWIS.GetWQValues(aStationNumber, aParamCode, aStartDate, aEndDate)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for GetWQURL</summary>
    <TestMethod(), DeploymentItem("D4EM.Data.Source.NWIS.dll")> Public Sub GetWQURLTest()
        Dim aStationNumber As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aStateFIPS As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aStartDate As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim aEndDate As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = NWIS_Accessor.GetWQURL(aStationNumber, aStateFIPS, aStartDate, aEndDate)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for FIPStoStateAbbrev</summary>
    <TestMethod(), DeploymentItem("D4EM.Data.Source.NWIS.dll")> Public Sub FIPStoStateAbbrevTest()
        Dim aFIPS As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = NWIS_Accessor.FIPStoStateAbbrev(aFIPS)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub
End Class
