Imports System.Xml
Imports System.Collections.Generic
Imports D4EM.Data
Imports atcUtility
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports D4EM.Data.Source

'''<summary>
'''This is a test class for BASINSTest and is intended
'''to contain all BASINSTest Unit Tests
'''</summary>
<TestClass()> _
Public Class BASINSTest
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
        Dim lBASINS As BASINS = New BASINS()
        Dim lQuerySchema As String = lBASINS.QuerySchema
        Assert.IsTrue(lQuerySchema.Contains("<DataExtension name='BASINS'>"))
        Assert.IsTrue(lQuerySchema.Contains("<function name='GetBASINS'"))
    End Sub

    '''<summary>Test for Name</summary>
    <TestMethod()> Public Sub NameTest()
        Dim lBASINS As BASINS = New BASINS()
        Assert.AreEqual("D4EM Data Download::BASINS", lBASINS.Name)
    End Sub

    '''<summary>Test for Description</summary>
    <TestMethod()> Public Sub DescriptionTest()
        Dim lBASINS As BASINS = New BASINS()
        Assert.AreEqual("Retrieve BASINS data", lBASINS.Description)
    End Sub

    '''<summary>Test for GetMetStations</summary>
    <TestMethod()> Public Sub GetMetStationsTest()
        Dim lProject As Project = CreateTestProject()
        Dim lStationIDs As List(Of String) = Nothing
        Dim lActual As String = BASINS.GetMetStations(lProject, lStationIDs, False)
        Dim lStationIDsExpected As New List(Of String) From
            {"GA090219", "GA090451", "GA090456", "GA091425", "GA091448",
             "GA092198", "GA092318", "GA093271", "GA093506", "GA093936",
             "GA094623", "GA094700", "GA094728", "GA095249", "GA095443",
             "GA095447", "GA095666", "GA095971", "GA095974", "GA095988",
             "GA096407", "GA098460", "GA098657", "GA098661", "GA098950",
             "GA099124", "GA099466", "GA722175", "GA722196"}
        Assert.AreEqual(lStationIDsExpected.Count, lStationIDs.Count)
        Dim lAllStationsMatch As Boolean = True
        For Each lStationId As String In lStationIDsExpected
            If Not lStationIDs.Contains(lStationId) Then
                 lAllStationsMatch = False
            End If
        Next
        Assert.IsTrue(lAllStationsMatch)
        Assert.AreEqual(String.Empty, lActual)
    End Sub

    '''<summary>Test for GetMetData</summary>
    <TestMethod()> Public Sub GetMetDataTest()
        Dim lProject As Project = CreateTestProject()
        Dim lStationIDs As New List(Of String) From {"GA090219"}
        Dim lMetWDM As String = lProject.ProjectFolder & "\met\met.wdm"
        Dim lActual As String = BASINS.GetMetData(lProject, lStationIDs, lMetWDM)
        Assert.IsTrue(lActual.Contains("<add_data type='Timeseries::WDM'>"))
        Assert.IsTrue(lActual.Contains("data\03070103\\met\met.wdm</add_data>"))
        Dim lActualBR As New IO.BinaryReader(IO.File.OpenRead(lMetWDM))
        Dim lExpectedLength As Int64 = 2170880
        Assert.AreEqual(lExpectedLength, lActualBR.BaseStream.Length)
    End Sub

    '''<summary>Test for GetBASINSSTATSGO</summary>
    <TestMethod()> Public Sub GetBASINSSTATSGOTest()
        Dim lProject As Project = CreateTestProject()
        Dim lActual As String = BASINS.GetBASINSSTATSGO(lProject)
        Assert.IsTrue(lActual.StartsWith("<message>STATSGO: Unpacked data from GA into"))
      End Sub

    '''<summary>Test for BASINS Constructor</summary>
    <TestMethod()> Public Sub BASINSConstructorTest()
        Dim lBasins As BASINS = New BASINS()
        Assert.AreEqual("D4EM Data Download::BASINS", lBasins.Name)
      End Sub

    '''<summary>Test for Execute</summary>
    <TestMethod()> Public Sub ExecuteTest()
        Dim lBasins As BASINS = New BASINS()
        Dim lQuery As String = String.Empty
        Dim lResult = lBasins.Execute(lQuery)
        Assert.AreEqual("<error>Root element is missing.</error>", lResult)
        'TODO: add more tests with a query string
    End Sub

    '''<summary>Test for GetBASINS</summary>
    <TestMethod()> Public Sub GetBASINSTest()
        Dim lProject As Project = CreateTestProject()
        Dim lHUC8 As String = gHuc8
        Dim lResult As String = BASINS.GetBASINS(lProject, Nothing, lHUC8, BASINS.BASINSDataType.core31)
        Assert.AreEqual(37, lResult.Split("<add_shape>").Count)
     End Sub
End Class
