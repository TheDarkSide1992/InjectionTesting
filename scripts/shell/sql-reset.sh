#!/bin/bash
set -e  # Cancel on errors

echo "♦️ Setting Up database Reset"

# PostgreSQL connection details
DB_CONN_STRING="postgresql://postgres:postgres@database:5432/InjectDb" #Change @<database> to if different databases

# Path to SQL folder
SQL_FOLDER="/scripts/sql"

echo "⏳ Waiting for connection to database..."
until pg_isready -h database -U postgres; do
  sleep 2
done

echo "🧹 Cleaning database..."
psql "$DB_CONN_STRING" -f "$SQL_FOLDER/ClearDatabase.sql"

echo "🔄 running Setting script on database..."
psql "$DB_CONN_STRING" -f "$SQL_FOLDER/Create.sql" #Consider changing the creation and cleanup scripts in case of a situation where the entire database is dropped
psql "$DB_CONN_STRING" -f "$SQL_FOLDER/MockData.sql"

echo "✅ Reset complete!"
