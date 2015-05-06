Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports D4EM.Data.Source

'''<summary>
'''This is a test class for NLDAS_NLDASGridCoordsTest and is intended
'''to contain all NLDAS_NLDASGridCoordsTest Unit Tests
'''</summary>
<TestClass()> _
Public Class NLDAS_NLDASGridCoordsTest
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

    '''<summary>Test for ToString</summary>
    <TestMethod()> Public Sub ToStringTest()
        Dim aYXString As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim target As NLDAS.NLDASGridCoords = New NLDAS.NLDASGridCoords(aYXString) ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = target.ToString
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>Test for NLDASGridCoords Constructor</summary>
    <TestMethod()> Public Sub NLDAS_NLDASGridCoordsConstructorTest()
        Dim aYXString As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim target As NLDAS.NLDASGridCoords = New NLDAS.NLDASGridCoords(aYXString)
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub

    '''<summary>Test for NLDASGridCoords Constructor</summary>
    <TestMethod()> Public Sub NLDAS_NLDASGridCoordsConstructorTest1()
        Dim aX As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim aY As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim target As NLDAS.NLDASGridCoords = New NLDAS.NLDASGridCoords(aX, aY)
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub
End Class
