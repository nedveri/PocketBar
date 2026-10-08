const url = require('url');
const path = require('path');
const { app, BrowserWindow } = require('electron');

let win;

function createWindow(){
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

app.on('ready', createWindow);

app.on("window-all-closed", () =>{
    app.quit();
})
