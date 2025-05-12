#!/bin/bash
set -e  # Stop the script on any error

echo "♦️ Setting Up Game Base database"

# PostgreSQL connection details
DB_USER="postgres"
POSTGRES_PASSWORD="postgres"
DB_NAME="InjectDb"

# Path to SQL folder
SQL_FOLDER="/scripts/sql"

export PGPASSWORD="POSTGRES_PASSWORD"

# Run specific SQL files
echo "🪄️ Setting PostgresSQL tables" # sets up tables
psql -U "$DB_USER" -d "$DB_NAME" -f "$SQL_FOLDER/Create.sql"

echo "🪄️ Setting PostgresSQL Mock Data" #Runs Moc Set up. #remove before publish
psql -U "$DB_USER" -d "$DB_NAME" -f "$SQL_FOLDER/MockData.sql"

echo "✅ Database setup complete!"
