echo f | xcopy /s /f "%~dp0..\..\Data\encrypted client files\l2.ini" l2.ini

echo x|mxencdec.exe l2.ini

move l2.ini "%~dp0..\..\Data\decrypted client files\l2.ini"

exit