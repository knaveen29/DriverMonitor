import "bootstrap/dist/css/bootstrap.min.css";
import { React } from "react";
import { useState, useEffect } from "react";
import { fetchDriverActivityData } from "../services/DataSimulationService.ts";
import DriverDetailsContainer from "./DriverDetailsContainer.jsx";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import { Container } from "react-bootstrap";

const DriverActivityDetails = () => {
  const currentDate = new Date();
  const [driversActivity, setDriversActivity] = useState([]);
  const [startDate, setStartDate] = useState(currentDate);
  const [endDate, setEndDate] = useState(currentDate);

  const fetchData = async () => {
    console.log("Fetching driver data - Begin");
    var response = await fetchDriverActivityData(startDate, endDate).then(
      (response) => {
        return response;
      }
    );
    console.log("Fetching driver data - Completed");
    setDriversActivity(response.violations);
  };

  const assignStartDate = (e) => {
    setStartDate(e.target.value);
  };

  const assignEndDate = (e) => {
    setEndDate(e.target.value);
  };

  const getActivityDataForGivenDates = () => {
    fetchData();
  };

  return (
    <div>
      <Container>
        <Row xs={12}>
          <Col xs={3}>
            Start Date
            <input
              id="startDate"
              className="form-control"
              type="date"
              name="startDate"
              onChange={assignStartDate}
              data-testid="start-date"
            ></input>
          </Col>
          <Col xs={3}>
            End date
            <input
              id="endDate"
              className="form-control"
              type="date"
              name="endDate"
              onChange={assignEndDate}
              data-testid="end-date"
            ></input>
          </Col>
          <Col xs={3}>
          <input
            className="btn btn-primary"
            data-testid="submit-btn"
            type="submit"
            onClick={getActivityDataForGivenDates}
          ></input>
          </Col>
        </Row>
      </Container>
<br></br>
      {driversActivity?.length === 0 ? (
        <h4>No data to display! Select a date range to get the drivers activities.</h4>
      ) : (
        <DriverDetailsContainer
          driversActivity={driversActivity}
        ></DriverDetailsContainer>
      )}
    </div>
  );
};
export default DriverActivityDetails;
