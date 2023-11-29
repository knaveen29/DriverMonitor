import { Table } from "react-bootstrap";
import { React } from "react";
import ActivityTable from "./ActivityTable.jsx";
import DriverDetailsCard from "./Card.jsx";
import "bootstrap/dist/css/bootstrap.min.css";
import Container from "react-bootstrap/Container";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import { VIOLATION_HEADERS } from "../constants/helper.ts";

const DriverDetailsContainer = ({ driversActivity }) => {
  return (
    <div>
      <h4><em>Driver activities</em></h4>
      <br></br>
      <Container>
        <Row>
          <Col>
            <DriverDetailsCard
              key={1}
              cardType={VIOLATION_HEADERS.SINGLE_DRIVE}
              driverDetails={
                driversActivity?.filter((item) => item.type === 0)[0]?.activity
              }
            ></DriverDetailsCard>
          </Col>
          <Col>
            <DriverDetailsCard
              key={2}
              cardType={VIOLATION_HEADERS.SINGLE_REST}
              driverDetails={
                driversActivity?.filter((item) => item.type === 1)[0]?.activity
              }
            ></DriverDetailsCard>
          </Col>
        </Row>
        <Row>
          <Col>
            <DriverDetailsCard
              key={3}
              cardType={VIOLATION_HEADERS.DAY_DRIVE}
              driverDetails={
                driversActivity?.filter((item) => item.type === 2)[0]?.activity
              }
            ></DriverDetailsCard>
          </Col>
          <Col>
            <DriverDetailsCard
              key={4}
              cardType={VIOLATION_HEADERS.WEEK_DRIVE}
              driverDetails={
                driversActivity?.filter((item) => item.type === 3)[0]?.activity
              }
            ></DriverDetailsCard>
          </Col>
        </Row>
      </Container>

      <>
        <ActivityTable
          driversActivity={driversActivity}
          type={0}
          title={VIOLATION_HEADERS.SINGLE_DRIVE}
        ></ActivityTable>
        <ActivityTable
          driversActivity={driversActivity}
          type={1}
          title={VIOLATION_HEADERS.SINGLE_REST}
        ></ActivityTable>
        <ActivityTable
          driversActivity={driversActivity}
          type={2}
          title={VIOLATION_HEADERS.DAY_DRIVE}
        ></ActivityTable>
        <ActivityTable
          driversActivity={driversActivity}
          type={3}
          title={VIOLATION_HEADERS.WEEK_DRIVE}
        ></ActivityTable>
      </>
    </div>
  );
};

export default DriverDetailsContainer;
