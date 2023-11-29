import { driverActivityApi } from '../../api/DriverActivityApi';
import * as mockData from "../../mockData/driverActivity";

  export const fetchMockResponse = () => Promise.resolve({
    ok: true,
    status: 200,
    json: async () => mockData.default
    } as Response);

    let fetchMock: any = undefined;

beforeEach(() => {
    fetchMock = jest.spyOn(global, "fetch")
    .mockImplementation(fetchMockResponse);
});

afterEach(() => {
    jest.clearAllMocks();
});

describe('Driver Activity Api', () => {
    it('should retrieve resume information', async () => {
        const actual  = await driverActivityApi.getActivityViolationData(new Date(), new Date());
        expect(fetchMock).toHaveBeenCalledTimes(1);
    });

    it('should reject promise when retrieve fails', async () => {
        fetchMock.mockReturnValue(Promise.reject('fake-reason'));

        await driverActivityApi.getActivityViolationData(new Date(), new Date()).catch((reason) => {
            expect(reason).toEqual('fake-reason');
        });
    });
});