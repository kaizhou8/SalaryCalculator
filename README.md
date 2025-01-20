# Salary Calculator

A .NET Core console application that calculates salary breakdowns based on gross package and pay frequency.

## Architecture & Design Decisions

### Domain-Driven Design (DDD)
- Used DDD patterns to model the salary calculation domain
- Core domain entities and value objects are immutable to ensure data integrity
- Rich domain model with business logic encapsulated in domain entities

### Clean Architecture
- Layered architecture for separation of concerns:
  - Domain Layer: Core business logic and entities
  - Application Layer: Use cases and application services
  - Infrastructure Layer: External concerns (I/O, persistence if needed)
  - Presentation Layer: Console UI and user interaction

### Design Patterns Used
1. Strategy Pattern: For different tax calculation strategies
2. Factory Pattern: Creating tax calculators and other domain objects
3. Command Pattern: Handling user inputs
4. Builder Pattern: Constructing complex salary breakdown objects
5. Decorator Pattern: For tax calculation chain

### Extensibility
The application is designed to be easily extended for:
- New tax rules and rates
- Additional deductions
- Different calculation methods
- Historical tax calculations
- Future tax rule changes

### Error Handling
- Custom domain exceptions for business rule violations
- Input validation using value objects
- Graceful error handling with user-friendly messages

### Testing
- Unit tests for core domain logic
- Integration tests for end-to-end scenarios
- Property-based testing for tax calculations

## Assumptions
1. All calculations are in Australian dollars
2. Tax rates are for the current financial year
3. Rounding rules follow ATO guidelines
4. Pay frequency calculations assume equal distribution across the year

## Future Improvements
1. Add configuration for tax rates and thresholds
2. Support for multiple financial years
3. Integration with ATO APIs for real-time rates
4. Support for additional allowances and deductions
5. Persistence layer for saving calculations
6. Web API interface for service integration
