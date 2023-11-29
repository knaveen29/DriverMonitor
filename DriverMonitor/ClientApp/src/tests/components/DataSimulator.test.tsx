import React from "react";
import { render, screen, fireEvent, act } from "@testing-library/react";
import "@testing-library/jest-dom/extend-expect";
import DataSimulator from "../../components/DataSimulator";
import * as DataSimulationService from "../../services/DataSimulationService";

jest.mock("../services/DataSimulationService");

describe("DataSimulator", () => {
    it("should render without crashing", () => {
      render(<DataSimulator />);
      
      expect(screen.getByRole('button', {'name' : /Start/i})).toBeInTheDocument();
      expect(screen.getByRole('button', {'name': /Stop/i})).toBeInTheDocument();
      expect(screen.getByText(/Data Generation Summary/i)).toBeInTheDocument();
    });
});