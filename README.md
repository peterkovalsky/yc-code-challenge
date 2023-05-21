### YC Code Challenge

The solution consists of RESTful API (.NET 7/C#) and the frontend app (React).

Steps to start the app:
- Start the api which is in `backend-api/YCCodeChallenge.API` folder. The server will start at this url https://localhost:7163.
- Navigate to `frontend/yc-super-app` folder and install all the frontend dependencies via `npm install`.
- Start the app with `npm start`

The React app already configured to use https://localhost:7163 but if you want to change it to a different url you can do so in `.env.development` file.



1. Since we cannot query data in Excel file we will have to read the whole file once and then query in-memory data to reduce I/O reads.