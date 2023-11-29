import { driverActivityApi } from "../../api/DriverActivityApi";
import { DriverActivityFilesValidationResponse } from "../../models/DriverActivityFilesValidationResponse";
import {
  fetchDriverActivityData,
  generateSampleData,
  getValidationResult,
} from "../../services/DataSimulationService";

jest.mock("../../api/DriverActivityApi");

describe("Data Simulation Service", () => {
  afterEach(() => {
    jest.clearAllMocks();
  });

  it("generateSampleData generates an array of driver files", () => {
    const numDrivers = 3;
    const result = generateSampleData(numDrivers);
    expect(result.length).toBeGreaterThan(numDrivers);
  });

  it("fetchDriverActivityData calls driverActivityApi.getActivityViolationData", async () => {
    const startDate = new Date();
    const endDate = new Date();
    const mockResponse = { data: "mocked data" };
    driverActivityApi.getActivityViolationData.mockResolvedValue(mockResponse);

    const result = await fetchDriverActivityData(startDate, endDate);

    expect(result).toEqual(mockResponse);
    expect(driverActivityApi.getActivityViolationData).toHaveBeenCalledWith(
      startDate,
      endDate
    );
  });

  it("getValidationResult calls driverActivityApi.validateActivityData", async () => {
    const driverFiles = [
      {
        /* sample driver file */
      },
    ];
    const mockResponse: DriverActivityFilesValidationResponse = {
      driverActivityFiles: [],
      isValidSimulation: true,
    };
    driverActivityApi.validateActivityData.mockResolvedValue(mockResponse);

    const result = await getValidationResult(driverFiles);

    expect(result).toEqual(mockResponse);
    expect(driverActivityApi.validateActivityData).toHaveBeenCalledWith(
      driverFiles
    );
  });
});
