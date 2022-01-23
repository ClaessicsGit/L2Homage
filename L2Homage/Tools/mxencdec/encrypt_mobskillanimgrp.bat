"%~dp0l2asm-disasm\l2asm.exe" -d "%~dp0l2asm-disasm\newdats\mobskillanimgrp-new.ddf" "%~dp0..\..\Data\decrypted client files\mobskillanimgrp.txt" "mobskillanimgrp.dat"
echo x|mxencdec.exe mobskillanimgrp.dat
move mobskillanimgrp.dat "%~dp0..\..\Export\Client"