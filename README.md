# HomeAwayAPITest
1) I consider it poor practice to have one test case rely on data from another test case however
since this exercise required it, that's how it's implemented.

2) I don't believe the Visual Studio/MSTest test execution engine guarantees a particular test case order however
in the testing I have done the cases were always run in the expected/necessary order.

3) The exercise asked to validate the full address including the country however I was unable to find a country
field that would facilitate this, so the verification excludes the country.

4) The project was built, tested and run in Visual Studio Professional 2013 on a Windows 7 machine.
  Also built, tested and run with the Microsoft .NET Framework version 4.5.51209.
  
The tests are built as Visual Studio Unit tests. They can be run from within Visual Studio itself or
run using MSTest.exe. 
To run within Visual Studio:
  1) Load the solution in Visual Studio
  2) Build the solution
  3) From the menu select TEST -> Run -> All Tests
  
To run from the command line:
  1) Copy the contents of the bin/Debug folder to a target directory on the test machine.
  2) If MSTest.exe is on your path simply run "MSTest.exe /testcontainer:homeawayapitest.dll".
    Note that MSTest.exe can generally be found at \Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\MSTest.exe
