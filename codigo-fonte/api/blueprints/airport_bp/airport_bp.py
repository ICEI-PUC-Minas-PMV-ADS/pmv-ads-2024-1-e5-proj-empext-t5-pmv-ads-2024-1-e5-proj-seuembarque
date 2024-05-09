from flask_sqlalchemy import SQLAlchemy 
from flask import request, jsonify, Blueprint
from models.airport import Airport
from config.config import db    
airport_bp = Blueprint("airport_bp", __name__)


@airport_bp.route("/aeroportos", methods=["GET"])
def get_all_airports():
    all_air = Airport.query.all()
    all_air_json = [x.to_json() for x in all_air]
    return jsonify(message="All the airports", data = all_air_json), 200

@airport_bp.route("/aeroporto/<int:airport_id>", methods=["GET"])
def get_airport(airport_id):
    airport = Airport.query.get(airport_id)
    if not airport:
        return jsonify(message="Airport not found"), 404
    else:
        airport_json = airport.to_json()
        return jsonify(message="Requested airport", data=airport_json), 200
    
@airport_bp.route("/aeroportos/filtrar", methods=["GET"])
def get_filters():
    filtered_airports = []

    for key, value in request.args.items():
        if hasattr(Airport, key):
            field = getattr(Airport, key)
            if key in ["name", "city", "country", "iata_code"]:
                airport_query = Airport.query.filter(field.ilike(f"%{value}%")).all()
            else:
                airport_query = Airport.query.filter(field == value).all()
            filtered_airports.extend(airport_query)

    a_json = [airport.to_json() for airport in filtered_airports]

    return jsonify(message="Results", data=a_json)
    
@airport_bp.route('/aeroporto', methods=["POST"])
def add_airport():
    data_air = request.get_json()
    name = data_air.get('name')
    city = data_air.get('city')
    country = data_air.get('country')
    iata_code = data_air.get('iata_code')
    latitude = data_air.get('latitude')
    longitude = data_air.get('longitude')
    links_count = data_air.get('links_count')

    new_air = Airport(name=name, city=city, country=country, 
                      iata_code=iata_code, latitude=latitude, 
                      longitude=longitude, links_count=links_count)
    db.session.add(new_air)
    new_air_json = new_air.to_json()
    return jsonify(message="Airport added successfully", data=new_air_json)
   

@airport_bp.route('/aeroporto', methods=["PUT"])
def update_airport():
    data_air = request.get_json()
    airport_id = data_air.get('airport_id')
    if not airport_id:
        return jsonify(message="You need to provide an id to update an airport"), 400
    up_airport = Airport.query.get(airport_id)
    if up_airport:
        for key, value in data_air.items():
            if key != "airport_id":
                setattr(up_airport, key, value)
        up_airport_json = up_airport.to_json()
        db.session.commit()
        return jsonify(data=up_airport_json, message="Airport updated"), 200
    else:
        return jsonify(message="Airport not found")

@airport_bp.route('/aeroporto/<int:airport_id>', methods=["DELETE"])
def delete_airport(airport_id):
    airport = Airport.query.get(airport_id)
    if not airport:
        return jsonify(message="Airport not found"), 404
    else:
        airport_json = airport.to_json()
        db.session.delete(airport)
        db.session.commit()
        return jsonify(message="Airport deleted successfully", data=airport_json)
        