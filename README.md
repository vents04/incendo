# Incendo - Secure and Unbiased Voting System

Incendo is a cryptographically secure voting and decision-making platform designed to ensure fairness, anonymity, and transparency in voting processes. The system uses advanced encryption and permutation techniques to eliminate bias and ensure that voting results cannot be manipulated.

## Core Features

- **Cryptographic Security**: End-to-end encryption using RSA and AES encryption standards
- **Multi-Phase Voting Process**: Robust voting process with separate phases for submission and verification
- **Permutation-Based Anonymization**: Uses cryptographic permutations to anonymize votes and prevent tracking of individual choices
- **Transparent Verification**: Complete auditability without compromising voter privacy
- **Campaign Management**: Create and manage various types of voting campaigns

## How It Works

The Incendo voting system uses a unique approach to ensure fairness:

1. **Campaign Creation**: An organization creates a voting campaign with specific configurations
2. **Permutation Generation**: Each participant generates a random permutation (reordering) of the voting options
3. **Encrypted Submission**: Participants submit their encrypted permutations without revealing the actual permutation
4. **Multi-Phase Process**: 
   - Modification Phase: Participants submit their encrypted modifications
   - Decryption Phase: Keys are revealed in a controlled manner
5. **Final Outcome**: The system combines all permutations to produce a final result that no single participant could have manipulated

This approach ensures that no single entity can know the complete voting process in advance, eliminating opportunities for tampering or bias.

## Project Structure

- **Web Components**:
  - `web-server`: Server-side components of the web application
  - `web-watcher`: Monitoring and observation tools
  - `web-general`: Shared web components
- **API Services**:
  - `ServerAPI`: Main server API for handling voting operations
  - `CampaignAPI`: API for managing voting campaigns
- **Core Components**:
  - `Data`: Data models and database interactions
  - `Services`: Business logic services

## Technical Details

Incendo employs several key technical concepts:

- **RSA Key Pairs**: For secure authentication and encryption
- **AES Encryption**: For encrypting the vote permutations
- **Cryptographic Hash Validation**: To verify the integrity of submitted data
- **Permutation Mathematics**: To anonymize and randomize voting processes

## Getting Started

1. Set up the database using the `dbSetup.sql` script
2. Follow migration instructions in `migrationsInstructions.txt`
3. Build and run the server components
4. Deploy the web applications

## Security Model

The security of Incendo is based on several principles:

1. **No Single Point of Trust**: The system is designed so that no single participant (including administrators) can manipulate the voting process
2. **Verifiable But Private**: All steps are cryptographically verifiable without revealing individual votes
3. **Phased Revelation**: Information is revealed in controlled phases to prevent gaming the system

## License

[License information would go here]

## Contact

[Contact information would go here] 