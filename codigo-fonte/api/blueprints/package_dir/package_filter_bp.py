from flask_sqlalchemy import SQLAlchemy
from flask import request, jsonify, Blueprint
from config.config import db
from models.package import Package
from models.client import Client

packages_filter_bp = Blueprint("packages_filter_bp", __name__)
@packages_filter_bp.route('/pacotes/filtrar', methods=["GET"])
def packages_filter():
    package_query = Package.query 
    for key in request.args.keys():
        value = request.args.get(key)    
        if hasattr(Package, key):                
            field = getattr(Package, key)
            if key == "origin" or key == "destination" or key == "travel_class":
                package_query = package_query.filter(field.ilike(f"%{value}%"))
            else:
                package_query = package_query.filter(field==value)    
    package_query_obj = package_query.all()
    package_query_json = []
    for package in package_query_obj:
        client_name = package.client.name if package.client else None
        client_email = package.client.email if package.client else None
        client_cpf = package.client.cpf if package.client and package.client.cpf else "preencher"
        client_cellphone = package.client.cellphone if package.client and package.client.cellphone else "preencher"             
        package_data_json = package.to_json()
        package_data_json['name'] = client_name
        package_data_json['email'] = client_email
        package_data_json['cpf'] = client_cpf
        package_data_json['cellphone'] = client_cellphone     
        package_query_json.append(package_data_json)  

    return jsonify(message="Requested packages", data= package_query_json), 200










    

