import React from 'react';
import { render } from '@testing-library/react';
import '@testing-library/jest-dom';
import ActivityTable from '../../components/ActivityTable';

const mockDriversActivity = [
  {
    type: 'testType',
    activity: [
      {
        driverId: '1',
        activity: 'Drive',
        activityDate: '2023-01-01',
        startDate: '2023-01-01',
        endDate: '2023-01-02',
      },
    ],
  },
];

describe('ActivityTable component', () => {
    it('renders ActivityTable with props', () => {
      const title = 'Test Title';
      const type = 'testType';
  
      const { getByText } = render(
        <ActivityTable type={type} driversActivity={mockDriversActivity} title={title} />
      );
  
      // Check if the title is rendered
      expect(getByText(title)).toBeInTheDocument();
  
      // Check if the table headers are rendered
      expect(getByText('Activity')).toBeInTheDocument();
      expect(getByText('Activity Date')).toBeInTheDocument();
      expect(getByText('Start Date')).toBeInTheDocument();
      expect(getByText('End Date')).toBeInTheDocument();
  
      // Check if the table row is rendered
      expect(getByText('1')).toBeInTheDocument();
      expect(getByText('Drive')).toBeInTheDocument();
    });
  });