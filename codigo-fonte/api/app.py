from flask import Flask, jsonify
from config.config import Config, db
from models.client import Client
from models.package import Package
from models.user import User
from models.airport import Airport
from blueprints.client_dir.client_crud_bp import clients_bp
from blueprints.client_dir.client_filter_bp import clients_filter_bp
from blueprints.package_dir.package_crud_bp import package_crud_bp
from blueprints.package_dir.package_filter_bp import packages_filter_bp
from blueprints.user_dir.user_crud_bp import user_bp
from blueprints.airport_bp.airport import airport_bp
from flask_migrate import Migrate

app = Flask(__name__)
app.config.from_object(Config)
db.init_app(app)
app.register_blueprint(clients_bp)
app.register_blueprint(clients_filter_bp)
app.register_blueprint(package_crud_bp)
app.register_blueprint(packages_filter_bp)
app.register_blueprint(user_bp)
app.register_blueprint(airport_bp)


Migrate(app, db)
if __name__ == "__main__":        
    app.run()





