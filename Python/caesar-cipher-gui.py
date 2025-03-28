#!/usr/bin/env python3
"""Create a Caesar Cipher GUI"""

import sys
from pathlib import Path

from PyQt5.QtWidgets import (
    QPushButton, 
    QApplication, 
    QMainWindow, 
    QTextEdit, 
    QAction, 
    QFileDialog, 
    QSpinBox
    )
from PyQt5 import QtCore
from PyQt5.QtGui import QFont

app = QApplication(sys.argv)

class Cipher(QMainWindow):
    #set window geometry
    def __init__(self):
        super().__init__()
        self.initUI()
        self.setGeometry(500, 200, 1000, 700)

    #set window content
    def initUI(self):
        self.setGeometry(300, 300, 600, 600)
        self.setWindowTitle('Ceasar Cipher')
        self.show()

        #textbox
        self.textEdit = QTextEdit()
        self.setCentralWidget(self.textEdit)
        self.textEdit.setStyleSheet(("background-color: pink; font-size: 12pt; font-family: Ink Free"))

        #Set file menu actions and add to file menu
        openAction = QAction('Open', self)
        openAction.setShortcut('Ctrl+O')
        openAction.setStatusTip('Open File')
        openAction.triggered.connect(self.open_file)

        saveAction = QAction('Save As', self)
        saveAction.setShortcut('Ctrl+Shift+S')
        saveAction.setStatusTip('Save File As')
        saveAction.triggered.connect(self.save_file)

        encryptAction = QAction('Encrypt', self)
        encryptAction.setShortcut('Ctrl+E')
        encryptAction.setStatusTip('Encrypt File')
        encryptAction.triggered.connect(self.encrypt_file)

        decryptAction = QAction('Decrypt', self)
        decryptAction.setShortcut('Ctrl+D')
        decryptAction.setStatusTip("Decrypt File")
        decryptAction.triggered.connect(self.decrypt_file)

        exitAction = QAction('Exit', self)
        exitAction.setShortcut('Ctrl+E')
        exitAction.setStatusTip('Exit')
        exitAction.triggered.connect(self.exit_file)

        menubar = self.menuBar() 
        menubar.setStyleSheet("background-color: deeppink; font-size: 14pt; font-family: Ink Free")
        fileMenu = menubar.addMenu('File')
        fileMenu.addAction(openAction)
        fileMenu.addAction(saveAction)
        fileMenu.addAction(encryptAction)
        fileMenu.addAction(decryptAction)
        fileMenu.addAction(exitAction)
        fileMenu.setStyleSheet("background-color: deeppink; font-size: 14pt; font-family: Ink Free")

        #Add encrypt/decrypt buttons
        _translate = QtCore.QCoreApplication.translate
        encryptButton = QPushButton('Encrypt', self)
        encryptButton.clicked.connect(self.encrypt_file)
        encryptButton.setGeometry(QtCore.QRect(100, 450, 75, 30))
        encryptButton.setText(_translate("MainWindow", "Encrypt"))
        encryptButton.setStyleSheet("background-color: deeppink; font-size: 14pt; font-family: Ink Free;")

        decryptButton = QPushButton(self)
        decryptButton.clicked.connect(self.decrypt_file)
        decryptButton.setGeometry(QtCore.QRect(300, 450, 75, 30))
        decryptButton.setText(_translate("MainWindow", "Decrypt"))
        decryptButton.setStyleSheet("background-color: deeppink; font-size: 14pt; font-family: Ink Free")

        #Add spinbox(rotation for cipher)
        self.spinBox = QSpinBox(self)
        self.spinBox.setMaximum(25)
        self.spinBox.setValue(0)
        self.spinBox.setStyleSheet("background-color: deeppink; font-size: 14pt; font-family: Ink Free")
    
        # Create a status bar and add the spin box and button widgets to it
        statusbar = self.statusBar()
        statusbar.addWidget(self.spinBox)
        statusbar.addWidget(encryptButton)
        statusbar.addWidget(decryptButton)
        statusbar.setStyleSheet("background-color: deeppink; font-size: 14pt; font-family: Ink Free")

    #Functions called when trigger/click
    #ln 100-117 derived from in class instruction 2/22/23
    def open_file(self):
        """Open text file from user selection and display in text box. """
        if sys.platform.startswith("win32"):
            # we're on windows
            initial_directory = Path.home() / "Documents"
        else:
            # we're on a unix-like os
            initial_directory = Path.home()

        filePath = QFileDialog().getOpenFileName(directory=str(initial_directory),
            filter="Text files (*.txt);;Python Source files (*.py);;All file types (*.*)",
            parent=self
        )[0]

        if filePath:
             with open(filePath, "r+") as f:
                self.textEdit.setText(f.read())

    def save_file(self):
        """Save file from text box to .txt file to user specified name and path"""
        filename = QFileDialog.getSaveFileName(self,
            filter="Text files (*.txt);;Python Source files (*.py);;All file types (*.*)",
        )[0]
        if filename:
            with open(filename, 'w') as file:
                file.write(self.textEdit.toPlainText())

    def encrypt_file(self):
        """
        Encrypt file using Caesar Cipher with rotation specified by 
        the user, using the spinBox.
        """
        key = self.spinBox.value()
        text = self.textEdit.toPlainText()
        cipher_text = ''
        for char in text:
            if char.isalpha():
                ascii_code = ord(char.lower())
                shifted_ascii_code = (ascii_code - 97 + key) % 26 + 97
                cipher_char = chr(shifted_ascii_code)
                cipher_text += cipher_char.upper() if char.isupper() else cipher_char
            else:
                cipher_text += char
        self.textEdit.setText(cipher_text)

    def decrypt_file(self):
        """
        Decrypt file using Caesar Cipher with rotation specified by 
        the user, using the spinBox.
        """
        key = self.spinBox.value()
        text = self.textEdit.toPlainText()
        plain_text = ''
        for char in text:
            if char.isalpha():
                ascii_code = ord(char.lower())
                shifted_ascii_code = (ascii_code - 97 - key) % 26 + 97
                plain_char = chr(shifted_ascii_code)
                plain_text += plain_char.upper() if char.isupper() else plain_char
            else:
                plain_text += char
        self.textEdit.setText(plain_text)

    def exit_file(self):
         """Exit window."""
         QApplication.exit()

cipher = Cipher()
sys.exit(app.exec_())