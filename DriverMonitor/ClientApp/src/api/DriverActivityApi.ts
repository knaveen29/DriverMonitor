import { DriverActivityFileRequest } from "../models/DriverActivityFile";

export class DriverActivityApi {
  private rootUrl = "http://localhost:5000/api/driver-activity";

  /**
   * Gets the driver activity files as per their violations for the given date range
   * @param start input start date
   * @param end input end date
   * @returns violation data for given date range
   */
  public async getActivityViolationData(start: Date, end: Date) {
    const startDate = new Date(start).toISOString();
    const endDate = new Date(end).toISOString();

    const url = `${this.rootUrl}/get-activity?startDate=${startDate}&endDate=${endDate}`;

    return await fetch(url, {
      method: 'GET',
      headers: {
        'Accept': 'text/plain',
        'Content-Type': 'application/json',
      }})
      .then((response) => {return response.json();})
      .then((responseJSON) => {return responseJSON;})
      .catch((error) => console.error(error));
  }

  /**
   * Validates the simulation data for given constraits
   * @param driverFiles List of simulated files to validate
   * @returns Returns the validation response if its successful or invalid
   */
  public async validateActivityData(driverFiles: DriverActivityFileRequest[]){
    
    const date = new Date().toISOString();
    const url = `${this.rootUrl}/validate-activities?simulationDate=${date}`;
    return await fetch(url, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(driverFiles)
    })
      .then((response) => {return response.json();})
      .then((responseJSON) => {return responseJSON;})
      .catch((error) => console.error(error));
  }

  /**
   * Adds a list of activity data
   * @param driverFiles list of activity files
   * @returns void
   */
  public async postData(driverFiles: DriverActivityFileRequest[]){
    const url = `${this.rootUrl}/post-files`;
    return await fetch(url, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(driverFiles)
    })
      .then((response) => {return response.json();})
      .then((responseJSON) => {return responseJSON;})
      .catch((error) => console.error(error));
  }
  
}

export const driverActivityApi: DriverActivityApi = new DriverActivityApi();