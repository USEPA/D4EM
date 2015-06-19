Imports System.Collections
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports D4EM.Geo

'''<summary>
'''This is a test class for HRUComparerTest and is intended
'''to contain all HRUComparerTest Unit Tests
'''</summary>
<TestClass()> _
Public Class HRUComparerTest

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

    '''<summary>Test for HRUComparer Constructor</summary>
    <TestMethod()> Public Sub HRUComparerConstructorTest()
        Dim target As HRUComparer = New HRUComparer()
        Assert.Inconclusive("TODO: Implement code to verify target")
    End Sub

    '''<summary>Test for Compare</summary>
    <TestMethod()> Public Sub CompareTest()
        Dim target As IComparer = New HRUComparer() ' TODO: Initialize to an appropriate value
        Dim x As Object = Nothing ' TODO: Initialize to an appropriate value
        Dim y As Object = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = target.Compare(x, y)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub
End Class
