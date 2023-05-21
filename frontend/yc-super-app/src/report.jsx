import { useState, useEffect } from "react";
import axios from "axios";
import { NumericFormat } from "react-number-format";

const QuarterlyReport = () => {
  const [reports, setReports] = useState(null);
  const [quarter, setQuarter] = useState(1);
  const [year, setYear] = useState(2018);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    getReportsAPI(quarter, year);
  }, []);

  const onQuarterChange = (selectedQuarter) => {
    setQuarter(selectedQuarter);
    getReportsAPI(selectedQuarter, year);
  };

  const onYearChange = (selectedYear) => {
    setYear(selectedYear);
    getReportsAPI(quarter, selectedYear);
  };

  const getReportsAPI = (quarter, year) => {
    setLoading(true);
    axios
      .get(
        `${process.env.REACT_APP_API_URL}/report/quarterly/${quarter}/${year}`
      )
      .then((response) => {
        setReports(response.data.employeeReports);
        setLoading(false);
      });
  };

  return (
    <div className="container">
      <h1 className="text-center">YC Quarterly Report</h1>

      <div className="col-4 offset-4">
        <div className="d-flex justify-content-between">
          <select
            class="form-select"
            value={quarter}
            onChange={(event) => onQuarterChange(event.target.value)}
          >
            <option value="1">Q1</option>
            <option value="2">Q2</option>
            <option value="3">Q3</option>
            <option value="4">Q4</option>
          </select>

          <select
            class="form-select"
            value={year}
            onChange={(event) => onYearChange(event.target.value)}
          >
            <option value="2017">2017</option>
            <option value="2018">2018</option>
            <option value="2019">2019</option>
            <option value="2020">2020</option>
          </select>
        </div>

        {loading && (
          <div class="spinner-border" role="status">
            <span class="visually-hidden">Loading...</span>
          </div>
        )}

        {!loading &&
          reports &&
          reports.map((report) => (
            <div className="card m-4">
              <div className="card-header">
                <strong>Employee {report.employeeCode}</strong>
              </div>
              <ul className="list-group list-group-flush">
                <li className="list-group-item d-flex justify-content-between">
                  Total OTE:{" "}
                  <NumericFormat
                    value={report.totalOTE}
                    displayType={"text"}
                    thousandSeparator={true}
                    prefix={"$"}
                  />
                </li>
                <li className="list-group-item d-flex justify-content-between">
                  Total Super Payable:
                  <NumericFormat
                    value={report.totalSuperPayable}
                    displayType={"text"}
                    thousandSeparator={true}
                    prefix={"$"}
                  />
                </li>
                <li className="list-group-item d-flex justify-content-between">
                  Total Disbursed:
                  <NumericFormat
                    value={report.totalDisbursed}
                    displayType={"text"}
                    thousandSeparator={true}
                    prefix={"$"}
                  />
                </li>
                <li className="list-group-item d-flex justify-content-between">
                  Variance:
                  <NumericFormat
                    value={report.variance}
                    displayType={"text"}
                    thousandSeparator={true}
                    prefix={"$"}
                  />
                </li>
              </ul>
            </div>
          ))}
      </div>
    </div>
  );
};

export default QuarterlyReport;
