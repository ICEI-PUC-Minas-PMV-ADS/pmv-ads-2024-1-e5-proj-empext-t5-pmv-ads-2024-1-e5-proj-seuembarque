from flask import Blueprint, jsonify, request
from config.config import db
from models.client import Client

clients_bp = Blueprint('clients_bp', __name__)

@clients_bp.route('/clientes', methods=["GET"])
def get_all_client():
    clients = Client.query.all()
    clients_json = [x.to_json() for x in clients]
    return jsonify(data=clients_json, message = "Todos os clientes"), 200

@clients_bp.route('/cliente/<int:client_id>', methods=["GET"])
def get_client(client_id):
    client = Client.query.get(client_id)
    client_json = client.to_json()
    return jsonify(data= client_json, message="Client located")


@clients_bp.route('/cliente', methods = ["POST"])
def add_client():
    new_client = request.get_json()
    if 'name' not in new_client or 'email' not in new_client:
        return jsonify(message="Name and email are mandatory. Please, enter them and try again."), 400
    name = new_client.get('name')
    email = new_client.get('email')
    cpf = new_client.get('cpf')
    cellphone = new_client.get("cellphone") 
    if cpf and cellphone:
        new_client_obj = Client(name=name, email=email, cpf=cpf, cellphone=cellphone)    
    elif not cpf and not cellphone:
        new_client_obj = Client(name=name, email=email)
    db.session.add(new_client_obj)
    db.session.commit()
    return jsonify(data=new_client_obj.to_json(), message="Client added successfully"), 201

@clients_bp.route('/cliente', methods=["PUT"])
def update_client():
    data_client = request.get_json()
    if 'client_id' in data_client:
        client_id = data_client.get('client_id')
        updated_client = Client.query.get(client_id)
    else: 
        return jsonify(message="You have to provide a client_id to update a client"), 400  
    for key, value in data_client.items():
        if key != 'client_id':
            setattr(updated_client, key, value)
    updated_client_json = updated_client.to_json()
    db.session.commit()
    return jsonify(data= updated_client_json, message="Client updated successfully"), 200

@clients_bp.route('/cliente', methods= ["DELETE"])
def delete_client():
    data_client = request.get_json()
    if 'client_id' not in data_client:
        return jsonify(message="You should provide a client_id to delete a client"), 400
    else:
        client_id = data_client.get('client_id')
    client_to_delete = Client.query.get(client_id)
    if not client_to_delete:
        return jsonify(message="Client not found"), 404
    db.session.delete(client_to_delete)
    client_to_delete_json = client_to_delete.to_json()
    return jsonify(data = client_to_delete_json, message="Client deleted successfully"), 200





    

















    
    



    
    
    