#!/bin/bash
set -e  # Afbryd ved fejl

echo "♦️ Setting Up database Reset"

# PostgreSQL connection details
DB_CONN_STRING="postgresql://postgres:postgres@database:5432/InjectDb" #Change @<database> to fir diferent databases

# Path to SQL folder
SQL_FOLDER="/scripts/sql"

echo "⏳ Waiting for connection to database..."
until pg_isready -h database -U postgres; do
  sleep 2
done

echo "🧹 Cleaning database..."
psql "$DB_CONN_STRING" -f "$SQL_FOLDER/ClearDatabase.sql"

echo "🔄 running Setting script on database..."
psql "$DB_CONN_STRING" -f "$SQL_FOLDER/MockData.sql"

echo "✅ Reset complete!"
