from models.package import Package 
from flask_sqlalchemy import SQLAlchemy
from config.config import db 
from flask import Blueprint, request, jsonify
from datetime import datetime

package_crud_bp = Blueprint('package_crud_bp', __name__)

@package_crud_bp.route('/pacotes', methods= ["GET"])
def get_all_packages():
    packages = Package.query.all()
    packages_json = [x.to_json() for x in packages]
    print(packages)
    return jsonify(data=packages_json, message="All packages"), 200

@package_crud_bp.route('/pacote/<int:package_id>', methods=['GET'])
def get_package(package_id):
    package = Package.query.get(package_id)
    if not package:
        return(jsonify(message="Package not found")), 404
    package_json = package.to_json()
    return jsonify(data=package_json, message="Requested package"), 200

@package_crud_bp.route('/pacote', methods=["POST"])
def add_package():
    package_data = request.get_json()
    client_id = package_data.get('client_id')
    origin = package_data.get('origin')
    destination = package_data.get('destination')
    departure_date = package_data.get('departure_date')    
    return_date = package_data.get('return_date')
    departure_date_obj = datetime.strptime(departure_date, "%d/%m/%Y")
    return_date_obj = datetime.strptime(return_date, "%d/%m/%Y")
    price = package_data.get('price')
    meals = package_data.get('meals')
    accomodation = package_data.get('accomodation')
    kids = package_data.get('kids')
    adults = package_data.get('adults')
    travel_class = package_data.get('travel_class')

    mandatory_fields = ["client_id", "origin", "destination", "departure_date", "return_date", "accomodation", "travel_class"]
    missing_fields = [x for x in mandatory_fields if x not in package_data]
    if missing_fields:
        return jsonify(message=f"You should provide {", ".join(missing_fields)}"), 400
    if return_date_obj < departure_date_obj:
        return jsonify(message="The return date must be after the departure date"), 400
    if meals and meals not in ["A", "C", "J", "ALL_ME", "ALL_IN"]:
        return jsonify(message="Invalid value for meals. It should be 'C' (breakfast), 'A', (lunch), 'J' (dinner) or 'ALL_ME' (all meals) or 'ALL_IN' (all inclusive)"),400 
    if travel_class not in ["economica", "executiva", 'primeira_classe']:
        return jsonify(message="Invalid value for travel_class. The available values are: 'economica', 'executiva', 'primeira_classe'"), 400
    new_package = Package(client_id=client_id, origin=origin, 
                          destination=destination, departure_date=departure_date_obj.strftime("%Y/%m/%d"),
                         return_date=return_date_obj.strftime("%Y/%m/%d"), price=price, meals=meals,
                         accomodation=accomodation, kids=kids, adults=adults, travel_class=travel_class)
    if new_package:
        db.session.add(new_package)
        db.session.commit()
        new_package_json = new_package.to_json()
        return jsonify(data=new_package_json, message="New package added"), 201
    

@package_crud_bp.route('/pacote', methods=["PUT"])
def update_package():
    data_package = request.get_json()
    package_id = data_package.get('package_id')
    if not package_id:
        return jsonify(message="You must provide a package_id in order to update a package"), 400
    
    updated_package = Package.query.get(package_id)
    
    for key, value in data_package.items():
        if key != "package_id" and key != "departure_date" and key != "return_date" and key != 'meals' and key != 'travel_class':
            setattr(updated_package, key, value)
    if "departure_date" in data_package or "return_date" in data_package:
        new_departure_date = data_package.get('departure_date')
        new_return_date = data_package.get('return_date')
        new_departure_date_form = datetime.strptime(new_departure_date, "%d/%m/%Y")
        new_return_date_form = datetime.strptime(new_return_date, "%d/%m/%Y")
        if new_return_date_form < new_departure_date_form:
            return jsonify(message="The return date must be after than the departure date "), 400
        updated_package.departure_date = new_departure_date_form.strftime('%Y/%m/%d')
        updated_package.return_date = new_return_date_form.strftime("%Y/%m/%d") 
    if 'meals' in data_package:    
        new_meals = data_package.get('meals')
        if new_meals not in ["C", "A", "J", "ALL"]:
            return jsonify(message="Invalid value for updating meals. Values should be 'C' (breakfast), 'A', (lunch), 'J' (dinner) or 'ALL' (all inclusive)"),400 
        else:
            updated_package.meals = new_meals
    if 'travel_class' in data_package:
        new_travel_class = data_package.get('travel_class')
        if new_travel_class not in ['economica', 'executiva', 'primeira_classe']:
            return jsonify(message="Invalid value for travel_class. The available values are: 'economica', 'executiva', 'primeira_classe'")
        else:   
            updated_package.travel_class = new_travel_class
    db.session.commit()
    return jsonify(message="Package updated successfully", data=updated_package.to_json()), 200

@package_crud_bp.route('/pacote', methods= ["DELETE"])
def delete_package():
    data_package = request.get_json()
    if "package_id" not in data_package:
        return jsonify(message="You should provide a package_id to delete a package"), 400
    else:
        package_id = data_package.get('package_id')
    package_to_delete = Package.query.get(package_id)
    if not package_to_delete:
        return jsonify(message="Package not found"), 404
    else:
        package_to_delete_json = package_to_delete.to_json()
    db.session.delete(package_to_delete)
    db.session.commit()
    return jsonify(data = package_to_delete_json, message = "Package successfully deleted"), 200
     


    

    

    

    
    

    
        
        
    





