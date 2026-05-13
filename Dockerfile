# שלב 1: בנייה (Build)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# העתקת קובץ ה-Solution והפרויקטים הרלוונטיים בלבד
COPY ["WebAPIShop.sln", "."]
COPY ["WebApiShop/WebApiShop.csproj", "WebApiShop/"]
COPY ["Service/Service.csproj", "Service/"]
COPY ["Repository/Repository.csproj", "Repository/"]
COPY ["Entity/Entities.csproj", "Entity/"]
COPY ["DTOs/DTOs.csproj", "DTOs/"]

# פתרון הבעיה: הסרת פרויקט הטסטים מקובץ ה-Solution בתוך ה-Docker
# פקודה זו מוחקת את פרויקט הטסטים מה-sln כדי שה-restore לא יחפש אותו
RUN dotnet sln WebAPIShop.sln remove TestProject/TestProject.csproj

# ביצוע Restore ללא פרויקט הטסטים
RUN dotnet restore "WebAPIShop.sln"

# העתקת שאר הקוד
COPY . .

# בנייה של ה-Solution (כעת ללא הטסטים)
RUN dotnet build "WebAPIShop.sln" -c Release -o /app/build

# שלב 2: פרסום (Publish)
FROM build AS publish
RUN dotnet publish "WebApiShop/WebApiShop.csproj" -c Release -o /app/publish /p:UseAppHost=false

# שלב 3: הרצה (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "WebApiShop.dll"]