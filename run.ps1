$CurrentPath = Get-Location

# cd ($CurrentPath.Path + "\")
# mvn clean install

cd $CurrentPath.Path

docker compose up -d --no-deps --build