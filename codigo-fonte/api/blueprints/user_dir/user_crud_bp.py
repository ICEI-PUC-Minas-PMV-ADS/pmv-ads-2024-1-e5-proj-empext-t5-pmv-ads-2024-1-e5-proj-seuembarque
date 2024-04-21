from flask_sqlalchemy import SQLAlchemy
from config.config import db 
from models.user import User
from flask import request, jsonify, Blueprint
from flask_bcrypt import Bcrypt

user_bp = Blueprint("user_crud_bp", __name__)

@user_bp.route('/usuarios/admin', methods=["GET"])
def get_users():
    users = User.query.all()
    if users:
        users_json = [x.to_json() for x in users]
        return jsonify(data=users_json, message="Requested users"), 200
    else:
        return jsonify(message="No users were found"), 404

@user_bp.route('/usuario/admin/<int:user_id>', methods=["GET"])
def get_user(user_id):
    user = User.query.get(user_id)
    if user:
        user_json = user.to_json()
        return jsonify(data=user_json, message="Requested user"), 200
    else:
        return jsonify(message="User not found"), 404
    
@user_bp.route('/usuario/admin', methods=["POST"])
def add_user():
    data_user = request.get_json()
    user_name = data_user.get('name')
    user_password = data_user.get('password')
    user_cellphone = data_user.get('cellphone')
    user_email = data_user.get('email')
    mandatory_fields = ["name", "password", "email"]
    missing_fields = [x for x in mandatory_fields if x not in data_user]
    if missing_fields:
        return jsonify(message=f"You have to provide {', '.join(missing_fields)}"), 400
    else:
        new_user = User(name=user_name, password=user_password, cellphone=user_cellphone, email=user_email)
    if new_user:
        db.session.add(new_user)
        db.session.commit()
    new_user_json = new_user.to_json()
    return jsonify(data=new_user_json, message="New user added successfully"), 201

@user_bp.route('/usuario/admin', methods=["PUT"])
def update_user():
    data_user = request.get_json()
    if "user_id" not in data_user:
        return jsonify(message="You have to provide an user_id to locate an user"), 400
    else:
        user_id = data_user['user_id']
        updated_user = User.query.get(user_id)
    if not updated_user:
        return jsonify(message="User not found"), 404
    else:
        for key, value in data_user.items():
            if key != "user_id" and key != "password":             
                setattr(updated_user, key, value)
    if "password" in data_user:
        new_password = data_user['password']
        bcrypt = Bcrypt()
        new_hashed_password = bcrypt.generate_password_hash(new_password)
        updated_user.password = new_hashed_password
    updated_user_json = updated_user.to_json()
    db.session.commit()

    return jsonify(message="User updated - the password is not displayed", data=updated_user_json), 200

@user_bp.route('/usuario/admin', methods=["DELETE"])
def delete_user():
    data_user = request.get_json()
    if "user_id" not in data_user:
        return jsonify(message="You have to provide an user_id to delete an user"), 400
    else:
        user_id = data_user.get('user_id')
        user_to_delete = User.query.get(user_id)
    if not user_to_delete:
        return jsonify(message="User not found"), 404
    else:
        user_to_delete_json = user_to_delete.to_json()
        db.session.delete(user_to_delete)
        db.session.commit()
        return jsonify(message="User deleted successfully", data=user_to_delete_json), 200

@user_bp.route("/usuario/admin/login", methods=["GET"])
def login_user():
    data = request.get_json()
    if "email" not in data or "password" not in data:
        return jsonify(message="You need to provide an email and a password to login"), 400
    user_email =  data.get('email')
    user_password = data.get('password')
    user = User.query.filter(User.email == user_email).first()
    if not user:
        return jsonify(message="User not found"), 404
    bcrypt = Bcrypt()
    login_try = bcrypt.check_password_hash(user.password, user_password)
    if login_try:
        user_json = user.to_json()
        return jsonify(message="Valid user and password", data=user_json, flag=login_try)
    else:
        return jsonify(message="Invalid password and/or user", flag=login_try)


    

        
            




    















    


    