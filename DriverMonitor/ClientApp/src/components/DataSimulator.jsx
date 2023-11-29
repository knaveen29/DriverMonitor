import { useState } from "react";
import React from "react";
import {
  generateSampleData,
  getValidationResult,
  saveFileData,
} from "../services/DataSimulationService.ts";
import { Container, Row, Col } from "react-bootstrap";
import Card from "react-bootstrap/Card";
import {
  DRIVERS_INPUT_LABEL,
  DATA_GENERATION_SUMMARY,
  TOTAL_GENERATED_DATA,
  BTN_TEXT_VALIDATE_DATA,
  DATA_VALIDATION_SUMMARY,
  TOTAL_VALIDATED_DATA,
} from "../constants/helper.ts";

const DataSimulator = () => {
  const [isSimulating, setIsSimulating] = useState(false);
  const [driverData, setDriverData] = useState([]);
  const [driverCount, setDriverCount] = useState(1);
  const [validatedData, setValidatedData] = useState({});

  const startLoop = () => {
    console.log("drivers selected" + driverCount);
    if(driverCount <= 1)
    {
      return;
    }
    setValidatedData([]);
    var data = {
      driverDetails: generateSampleData(driverCount),
    };
    console.log("data setting to card" + JSON.stringify(data));
    setDriverData(data.driverDetails);
    setIsSimulating(true);
  };

  const handleSubmit = (event) => {
    console.log(event);
    saveFileData(driverData);
    event.preventDefault();
  };

  const stopLoop = () => {
    setIsSimulating(false);
  };

  const validateData = async () => {
    console.log("Trying to validate data");
    setValidatedData([]);
    let result = await getValidationResult(driverData);
    if (result !== undefined) {
      console.log("Setting validation response");
      setValidatedData(result);
    }
  };

  return (
    <div>
      <form
        onSubmit={(e) => {
          e.preventDefault();
        }}
      >
        <Container>
          <Row xs={6}>
            <Col xs={6}>
              <Col xs={12}>
                {DRIVERS_INPUT_LABEL}&nbsp;&nbsp;
                <input
                  type="number"
                  name="drivers"
                  min={1}
                  max={100}
                  onChange={(e) => setDriverCount(e.target.value)}
                ></input>
                &nbsp;&nbsp;
                <button
                  className="btn btn-primary mt-2"
                  name="start-btn"
                  data-testid="start-btn"
                  onClick={startLoop}
                >
                  Start
                </button>
                &nbsp;&nbsp;
                <button
                  className="btn btn-warning mt-2"
                  name="stop-btn"
                  data-test-id="stop-btn"
                  onClick={stopLoop}
                >
                  Stop
                </button>
              </Col>
            </Col>
          </Row>
        </Container>
        <br></br>
        <Container>
          <Card
            bg={"dark"}
            key={"secondary"}
            text={"secondary".toLowerCase() === "light" ? "dark" : "white"}
            style={{ width: "50rem" }}
            className={"mb-2"}
          >
            <Card.Body>
              <Card.Title>{DATA_GENERATION_SUMMARY}</Card.Title>
              <Card.Text>
                {TOTAL_GENERATED_DATA} {driverData.length}
              </Card.Text>
            </Card.Body>
          </Card>
          <button
            className="btn btn-success"
            type="submit"
            onClick={validateData}
          >
            {BTN_TEXT_VALIDATE_DATA}
          </button>
        </Container>
      </form>

      <br></br>
      <br></br>
      <form>
        <Container
          style={
            validatedData.isValidSimulation == undefined
              ? { display: "none" }
              : {}
          }
        >
          <Card
            bg={validatedData.isValidSimulation ? "primary" : "warning"}
            key={"success"}
            text={"secondary".toLowerCase() === "light" ? "dark" : "white"}
            style={{ width: "50rem" }}
            className={"mb-2"}
          >
            <Card.Body>
              <Card.Title>{DATA_VALIDATION_SUMMARY}</Card.Title>
              <Card.Subtitle>
                Result :{" "}
                {validatedData.isValidSimulation
                  ? "Data valid"
                  : "Data invalid"}
              </Card.Subtitle>
              <Card.Text>
                {TOTAL_VALIDATED_DATA}{" "}
                {validatedData?.driverActivityFiles?.length}
              </Card.Text>
            </Card.Body>
          </Card>
          <button
            className="btn btn-success"
            type="submit"
            onClick={handleSubmit}
          >
            Submit
          </button>
        </Container>
      </form>
    </div>
  );
};

export default DataSimulator;
