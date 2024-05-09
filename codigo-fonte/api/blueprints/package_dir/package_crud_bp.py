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
    departure_date_str = package_data.get('departure_date')
    return_date_str = package_data.get('return_date')
    departure_date = datetime.strptime(departure_date_str, "%d/%m/%Y")
    if return_date_str:
        return_date = datetime.strptime(return_date_str, "%d/%m/%Y")
        if return_date < departure_date:
            return jsonify(message="The return date must be after the departure date"), 400
        return_date_form = return_date.strftime("%Y/%m/%d")
    departure_date_form = departure_date.strftime("%Y/%m/%d")

    price = package_data.get('price')
    meals = package_data.get('meals')
    accommodation = package_data.get('accommodation')
    kids = package_data.get('kids')
    adults = package_data.get('adults')
    travel_class = package_data.get('travel_class')
    mandatory_fields = ["client_id", "origin", "destination", "departure_date",  "accommodation", "travel_class"]
    missing_fields = [x for x in mandatory_fields if x not in package_data]
    if missing_fields:
        return jsonify(message=f"You should provide {', '.join(missing_fields)}"), 400
   
    # if meals and meals not in ["A", "C", "J", "ALL_ME", "ALL_IN"]:
    #     return jsonify(message="Invalid value for meals. It should be 'C' (breakfast), 'A', (lunch), 'J' (dinner) or 'ALL_ME' (all meals) or 'ALL_IN' (all inclusive)"),400 
    if travel_class not in ["economica", "executiva", 'primeira_classe']:
        return jsonify(message="Invalid value for travel_class. The available values are: 'economica', 'executiva', 'primeira_classe'"), 400
    if return_date_str:
        new_package = Package(client_id=client_id, origin=origin, 
                          destination=destination,
                         departure_date=departure_date_form,
                         return_date=return_date_form, price=price, meals=meals,
                         accommodation=accommodation, kids=kids, adults=adults, 
                         travel_class=travel_class)
    else:
        new_package = Package(client_id=client_id, origin=origin, 
                          destination=destination,
                         departure_date=departure_date_form,
                         price=price, meals=meals,
                         accommodation=accommodation, kids=kids,
                           adults=adults, travel_class=travel_class)
   
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
    if not updated_package:
        return jsonify(message="Package not found"), 404
        
    for key, value in data_package.items():
        if key != "package_id" and key != "departure_date" and key != "return_date":
            setattr(updated_package, key, value)    
    db.session.commit()
    if "departure_date" in data_package and "return_date" not in data_package:
        departure_date_str = data_package.get('departure_date')
        if departure_date_str:
            departure_date_obj = datetime.strptime(departure_date_str, "%d/%m/%Y").date()
            updated_package.departure_date = departure_date_obj.strftime("%Y/%m/%d")
            db.session.commit()
            return jsonify(data=updated_package.to_json())
        else:
            updated_package.departure_date = departure_date_str
            db.session.commit()
            return jsonify(data=updated_package.to_json()), 200
    if "return_date" in data_package and "departure_date" not in data_package:
        return_date_str = data_package.get('return_date')
        if return_date_str:
            return_date_obj = datetime.strptime(return_date_str, "%d/%m/%Y").date()
            updated_package.return_date = return_date_obj.strftime("%Y/%m/%d")
            db.session.commit()
            return jsonify(data=updated_package.to_json()), 200
        else:
            updated_package.return_date = return_date_str
            db.session.commit()
            return jsonify(data=updated_package.to_json()), 200
        
    if "departure_date" in data_package and "return_date" in data_package:
        departure_date_str = data_package.get('departure_date')
        return_date_str = data_package.get('return_date')
        if departure_date_str and return_date_str:
            departure_date_obj = datetime.strptime(departure_date_str, "%d/%m/%Y").date()
            updated_package.departure_date = departure_date_obj.strftime("%Y/%m/%d")
            return_date_obj = datetime.strptime(return_date_str, "%d/%m/%Y").date()
            updated_package.return_date = return_date_obj.strftime("%Y/%m/%d")
            db.session.commit()
            return jsonify(data=updated_package.to_json()), 200
        if not departure_date_str and not return_date_str:
            updated_package.departure_date = departure_date_str
            updated_package.return_date = return_date_str
            db.session.commit()
            return jsonify(data=updated_package.to_json()), 200
        if departure_date_str and not return_date_str:
            departure_date_obj = datetime.strptime(departure_date_str, "%d/%m/%Y").date()
            updated_package.departure_date = departure_date_obj.strftime("%Y/%m/%d")
            updated_package.return_date = return_date_str
            db.session.commit()
            return jsonify(data=updated_package.to_json())
            
  

@package_crud_bp.route('/pacote/<int:package_id>', methods= ["DELETE"])
def delete_package(package_id):
    package = Package.query.get(package_id)
    if package_id:
        package_json = package.to_json()
        db.session.delete(package)
        db.session.commit()
        return jsonify(data=package_json, message="Package deleted successfully"), 200
    else:
        return jsonify(message="Package not found"), 404







    

    

    

    
    

    
        
        
    





