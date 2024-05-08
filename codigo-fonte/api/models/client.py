from flask_sqlalchemy import SQLAlchemy
from config.config import db
from datetime import datetime

class Client(db.Model):
    __tablename__ = "clients_table"
    client_id = db.Column(db.Integer, primary_key = True, autoincrement=True)
    name = db.Column(db.String(200))
    email = db.Column(db.String(100))
    cpf = db.Column(db.String(12), nullable=True, default="preencher")
    cellphone = db.Column(db.String(50), nullable = True, default = "preencher")
    registration_date = db.Column(db.DateTime)
    packages = db.relationship("Package", backref="client", cascade="all, delete-orphan")


    def __init__(self, name, email, cpf, cellphone):
        self.name = name 
        self.email = email
        self.registration_date = datetime.now()
        self.cpf = cpf
        self.cellphone = cellphone

    def __repr__(self):
        return f"Cliente {self.name}, email {self.email}"
    
    def to_json(self):
        return {
        "client_id": self.client_id,
        "name": self.name, 
        "email": self.email,
        "cpf": self.cpf if self.cpf is not None else None,
        "registration_date": self.registration_date.strftime("%d/%m/%Y %H:%M:%S") if self.registration_date is not None else None, 
        "cellphone": self.cellphone if self.cellphone is not None else None } 
        
