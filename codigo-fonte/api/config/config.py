from flask_sqlalchemy import SQLAlchemy

db = SQLAlchemy()

class Config:    
    SQLALCHEMY_DATABASE_URI = 'mysql+mysqlconnector://joseccosta:esvMtEmPw9H#_6J@joseccosta.mysql.pythonanywhere-services.com/joseccosta$seu_embarque_db'
    SQLALCHEMY_TRACK_MODIFICATIONS = False 

