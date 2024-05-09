"""empty message

Revision ID: 706641e92fa3
Revises: 6528c41dbe17
Create Date: 2024-05-08 13:24:27.793212

"""
from alembic import op
import sqlalchemy as sa


# revision identifiers, used by Alembic.
revision = '706641e92fa3'
down_revision = '6528c41dbe17'
branch_labels = None
depends_on = None


def upgrade():
    # ### commands auto generated by Alembic - please adjust! ###
    with op.batch_alter_table('packages_table', schema=None) as batch_op:
        batch_op.drop_constraint('packages_table_ibfk_1', type_='foreignkey')
        batch_op.create_foreign_key(None, 'clients_table', ['client_id'], ['client_id'], ondelete='CASCADE')

    # ### end Alembic commands ###


def downgrade():
    # ### commands auto generated by Alembic - please adjust! ###
    with op.batch_alter_table('packages_table', schema=None) as batch_op:
        batch_op.drop_constraint(None, type_='foreignkey')
        batch_op.create_foreign_key('packages_table_ibfk_1', 'clients_table', ['client_id'], ['client_id'])

    # ### end Alembic commands ###