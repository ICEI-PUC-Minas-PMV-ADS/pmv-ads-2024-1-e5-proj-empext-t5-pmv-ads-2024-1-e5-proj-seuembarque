from flask_sqlalchemy import SQLAlchemy
from config.config import db 

class Airport(db.Model):
    __tablename__ = "airports_table"
    airport_id = db.Column(db.Integer, primary_key=True, autoincrement=True)
    name = db.Column(db.String(150))
    city = db.Column(db.String(150))
    country = db.Column(db.String(100))
    iata_code = db.Column(db.String(5))
    latitude = db.Column(db.Float)
    longitude = db.Column(db.Float)
    links_count = db.Column(db.Integer)  
    links_count = db.Column(db.Float)

    def __init__(self, name, city, country, iata_code,
                  latitude, longitude, links_count):
        self.name = name
        self.city = city
        self.country = country
        self.iata_code = iata_code
        self.latitude = latitude        
        self.longitude = longitude
        self.links_count = links_count

    def to_json(self):
        return {
            "airport_id": self.airport_id,
            "name": self.name,
            "country": self.country,
            "city": self.city,
            "iata_code": self.iata_code,
            "latitude": self.latitude,
            "longitude": self.longitude,
            "links_count": self.links_count
        }

        