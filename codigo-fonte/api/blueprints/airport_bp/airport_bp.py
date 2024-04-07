from flask_sqlalchemy import SQLAlchemy 
from flask import request, jsonify, Blueprint
from models.airport import Airport

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
        return jsonify(message="Requested airport"), 200
    
@airport_bp.route("/aeroportos/filtrar", methods = ["GET"])
def get_filters():
    for key in request.args.keys():
        value = request.args.get(key)
        if hasattr(Airport, key):
            print(f"printando {key}")
            field = getattr(Airport, key)
            if key == "name" or key == "city"  or key == "country" or key == "iata_code":
                print(f"printando key pela segunda vez {key}")
                airport_query = Airport.query.filter(field.ilike(f"%{value}"))
            else:
                airport_query = Airport.query.filter(field == value)
        airport_query_obj = airport_query.all()
        a_json = [x.to_json() for x in airport_query_obj]                    

    return jsonify(message="Results", data = a_json) 









    
    

