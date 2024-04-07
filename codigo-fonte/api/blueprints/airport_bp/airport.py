from flask_sqlalchemy import SQLAlchemy 
from flask import request, jsonify, Blueprint
from models.airport import Airport

airport_bp = Blueprint("airport_bp", __name__)


@airport_bp.route("/aeroportos", methods=["GET"])
def get_all_airports():
    all_air = Airport.query.all()
    all_air_json = [x.to_json() for x in all_air]
    return jsonify(message="All the airports", data = all_air_json), 200
    
    

