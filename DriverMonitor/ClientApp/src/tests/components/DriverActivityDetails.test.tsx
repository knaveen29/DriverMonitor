import React from "react";
import { render, screen, fireEvent, waitFor } from "@testing-library/react";
import "@testing-library/jest-dom/extend-expect"; // for expect assertions
import * as DataSimulationService from "../../services/DataSimulationService";
import * as mockData from "../../mockData/driverActivity";
import DriverActivityDetails from "../../components/DriverActivityDetails";
import { act } from "react-dom/test-utils";
import reactMock from "react";

jest.mock("../../services/DataSimulationService");

const setHookState = (newState) =>
  jest.fn().mockImplementation(() => [newState, () => {}]);


describe("DriverActivityDetails", () => {
  it("renders without crashing", () => {
    render(<DriverActivityDetails />);
    expect(screen.getByText(/Start Date/i)).toBeInTheDocument();
    expect(screen.getByText(/End date/i)).toBeInTheDocument();
    expect(screen.getByRole("button", { name: /submit/i })).toBeInTheDocument();
  });

  it("fetches driver activity data when button is clicked", async () => {
    (
      DataSimulationService.fetchDriverActivityData as jest.MockedFunction<
        typeof DataSimulationService.fetchDriverActivityData
      >
    ).mockResolvedValueOnce(mockData);
    render(<DriverActivityDetails />);

    fireEvent.change(await screen.getByTestId(/start-date/i), {
      target: { value: new Date() },
    });

    fireEvent.change(await screen.findByTestId(/end-date/i), {
      target: { value: new Date() },
    });

    fireEvent.click(screen.getByRole("button", { name: /submit/i }));

    expect(screen.getByText(/No data to display/i)).toBeInTheDocument();
  });
});
