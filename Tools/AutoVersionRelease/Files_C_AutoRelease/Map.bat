@ECHO off

if EXIST I:\ (
	subst I: /D
)
subst I: C:\AutoRelease\EFVS\Software

IF EXIST J:\ (
	subst J: /D
)
subst J: C:\AutoRelease\EFVS\Software\Projects\Dassault