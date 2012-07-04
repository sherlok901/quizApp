function WriteToFile(str) {

    var objShell = new ActiveXObject("Wscript.Shell");
    strPath = objShell.CurrentDirectory;

    var objFileSystem = new ActiveXObject("Scripting.fileSystemObject");
    var strOutputFile = strPath + "\\KeysOutput.txt";

    if (typeof WriteToFile.counter == 'undefined') {
        WriteToFile.counter = 0;

        var objOutputFile = objFileSystem.CreateTextFile(strOutputFile, true)
        objOutputFile.Close()
    }

    var objOutputFile = objFileSystem.OpenTextFile(strOutputFile, 8, true);
    objOutputFile.WriteLine(str);
    objOutputFile.close();
}


function Mess(str) {
    Debug.Print(str);
    WriteToFile(str);
}