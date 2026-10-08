const url = require('url');
const path = require('path');
const { app, BrowserWindow, ipcMain } = require('electron');
const { execFile } = require('child_process'); 

let win;
let cSharpProcess;

function createWindow(){

const backendPath = path.join(__dirname, 'MyBackend.exe');
    
    cSharpProcess = execFile(backendPath, (error, stdout, stderr) => {
        if (error) {
            console.error('Ошибка запуска C# бэкенда:', error);
        }
    });



    win = new BrowserWindow({
         width: 16*80,
         height: 9*80,
            minWidth: 900,
            minHeight: 700,
            titleBarStyle: 'hidden',
            ...(process.platform !== 'darwin' ? { 
                titleBarOverlay: true,
                icon: path.join(__dirname, 'icon.ico')
            } : {}),
         webPreferences: {
             nodeIntegration: true,
             contextIsolation: false 
         }
    });

    win.loadURL(url.format({
        pathname: path.join(__dirname, 'html/index.html'),
        protocol: 'file:',
        slashes: true
    }));
}

ipcMain.on('window-minimize', () => {
    if (win) win.minimize();
});

ipcMain.on('window-close', () => {
    if (win) win.close();
});

app.on('ready', createWindow);

app.on("window-all-closed", () =>{
    app.quit();
    if(cSharpProcess) {
        cSharpProcess.kill();
    }
})
