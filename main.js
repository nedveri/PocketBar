const url = require('url');
const path = require('path');
const { app, BrowserWindow } = require('electron');

let win;

function createWindow(){
    win = new BrowserWindow({
         width: 16*50,
         height: 9*50,
            minWidth: 400,
            minHeight: 300,
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
        pathname: path.join(__dirname, 'test.html'),
        protocol: 'file:',
        slashes: true
    }));
}

app.on('ready', createWindow);

app.on("window-all-closed", () =>{
    app.quit();
})
