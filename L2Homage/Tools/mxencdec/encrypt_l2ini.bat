echo f | xcopy /s /f "%~dp0..\..\Data\decrypted client files\l2.ini" l2.dat

echo x|mxencdec.exe l2.dat

ren l2.dat l2.ini

move l2.ini "%~dp0..\..\Export\Client"