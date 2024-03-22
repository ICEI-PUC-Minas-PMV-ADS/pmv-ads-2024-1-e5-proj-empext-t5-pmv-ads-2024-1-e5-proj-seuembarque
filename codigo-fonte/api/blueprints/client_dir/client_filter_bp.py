from config.config import db 
from flask import request, Blueprint, jsonify
from models.client import Client
from datetime import datetime

clients_filter_bp = Blueprint('clients_filter_bp', __name__)

@clients_filter_bp.route('/clientes/filtrar', methods=["GET"])
def filter_client():
    name = request.args.get('name')
    email = request.args.get('email')
    cpf = request.args.get('cpf')
    cellphone = request.args.get('cellphone')
    registration_date = request.args.get('date')
    if name:
        cl_query = Client.query.filter(Client.name.ilike(f"%{name}%"))
    if email:
        cl_query = Client.query.filter(Client.email.ilike(f"%{email}%"))
    if cpf:
        cl_query = Client.query.filter(Client.cpf.ilike(f"%{cpf}%"))
    if cellphone:
        cl_query = Client.query.filter(Client.cellphone.ilike(f"%{cpf}%"))
    if registration_date:
        try:
            registration_date = datetime.strptime(registration_date, '%d/%m/%Y').date()            
            registration_date_formatted = registration_date.strftime('%Y-%m-%d')
            cl_query = Client.query.filter(Client.registration_date == registration_date_formatted)
        except ValueError:
            return jsonify(message="Invalid format date. Enter 'dd/mm/YYYY' format."), 400
    cl_query_obj = cl_query.all()
    cl_query_json = [x.to_json() for x in cl_query_obj]
    return jsonify(data=cl_query_json, message="Results")

    


