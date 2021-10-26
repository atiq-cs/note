Title: Creating Token, NFT using Solana
Lead: Solana CLI Usage
Published: 10/24/2021
Tags:
  - solana
  - crypto
---

Using the Token Program we are able to create fungible and non-fungible tokens.

### Pre-requisites
We need the token program to be able to accomplish those. We follow official documentation: [SPL Solana Token](https://spl.solana.com/token). We need a Linux Machine or VM with about 16GB or more main memory. Free tier aws linux vm doesn't work.

- Install Rust

      curl --proto '=https' --tlsv1.2 -sSf https://sh.rustup.rs | sh

- Install Solana CLI

As per [solana docs - install-solana-cli-tools](https://docs.solana.com/cli/install-solana-cli-tools) we install solana cli and export its installation location into `PATH` variable.


    sh -c "$(curl -sSfL https://release.solana.com/v1.8.1/install)"
    export PATH="/home/atiq/.local/share/solana/install/active_release/bin:$PATH"


Install required packages for building `spl-token-cli`,

    sudo dnf install openssl-devel --assumeyes
    sudo dnf install libudev-devel --assumeyes

Finally, we install the token library program,

    cargo install spl-token-cli

aws free tier linux VM usually hit following OOM error,

    could not compile libsecp256k1 signal: 9, SIGKILL: kill

Once the build is successful, we check config,

    $ solana config get
    Config File: /home/atiq/.config/solana/cli/config.yml
    RPC URL: https://api.mainnet-beta.solana.com 
    WebSocket URL: wss://api.mainnet-beta.solana.com/ (computed)
    Keypair Path: /home/atiq/.config/solana/id.json 
    Commitment: confirmed 

By default, RPC is set to mainnet. Let's change it to dev so that we can do some interesting experiments.

    $ solana config set --url https://api.devnet.solana.com
    Config File: /home/atiq/.config/solana/cli/config.yml
    RPC URL: https://api.devnet.solana.com 
    WebSocket URL: wss://api.devnet.solana.com/ (computed)
    Keypair Path: /home/atiq/.config/solana/id.json 
    Commitment: confirmed 

To get keypair let's create a FileSystem Wallet.

## Creating File System Wallet
main reference: https://docs.solana.com/wallet-guide/file-system-wallet

It is insecure because the keypair files are unencrypted.

Let's create key pair,

    $ mkdir ~/solana-wallet

    $ solana-keygen new --outfile ~/solana-wallet/keypair.json
    Generating a new keypair
    For added security, enter a BIP39 passphrase

    NOTE! This passphrase improves security of the recovery seed phrase NOT the
    keypair file itself, which is stored as insecure plain text

    BIP39 Passphrase (empty for none): 
    Wrote new keypair to /home/atiq/solana-wallet/keypair.json
    ==============================================================================
    pubkey: 5cESBj8D4d6heCiYTKxJSs5Adtbf9rsFedVoZJnRRM7c
    ==============================================================================
    Save this seed phrase and your BIP39 passphrase to recover your new keypair:
    key battle mystery control bonus manual thought jazz kidney slam plastic token
    ==============================================================================

Verify, just in case,

    solana-keygen verify 5cESBj8D4d6heCiYTKxJSs5Adtbf9rsFedVoZJnRRM7c ~/solana-wallet/keypair.json


## Creating Tokens
If we have successfully previous sections we can create Tokens now.

Let's set keypair in solana config that we got from file system wallet (previous section),
    
    $ solana config set --keypair $HOME/solana-wallet/keypair.json
    Config File: /home/atiq/.config/solana/cli/config.yml
    RPC URL: https://api.devnet.solana.com 
    WebSocket URL: wss://api.devnet.solana.com/ (computed)
    Keypair Path: /home/atiq/solana-wallet/keypair.json 
    Commitment: confirmed

We need some SOL to perform our next actions.

    $ solana airdrop 1
    Requesting airdrop of 1 SOL
    Signature: 6GVKHvFdBrTC93i4m7Jp2o12VLVsSyJFYHHBvcXBMwSsa8MBz83TCnfDgcijrUHBcuZrsyud4d5oUrBFMfUyt1tM
    1 SOL

### Creating Fungible Token
Let's create token,

    spl-token create-token
    Creating token GjvWk83m1vKQjmQU2dPnLTRvAKjtAKsAmKKNu7Gyk93
    Signature: 2ivgjD3N7xpj2vhDg4YoF3gYMAwkytmbydYeyWVRLzFuUYusUZ4oko6eLTMAws4NKW9YtUuFTcMfEQHYiwgceaCD

Mint 100 new tokens,

    spl-token mint GjvWk83m1vKQjmQU2dPnLTRvAKjtAKsAmKKNu7Gyk93 100
    Minting 100 tokens
    Token: GjvWk83m1vKQjmQU2dPnLTRvAKjtAKsAmKKNu7Gyk93
    Recipient: F8xUCvGx19sZJVy1cMkZgFRMmU7DWCyXTzDebDzDPnwv
    Signature: 5i2y9eBAYipJVGsqU6fLR8q61CgJpX1B9iU6BdHh5iPcE9JtefsGPJ7nKQuEjGSqVxqXKHAPC5hPFp7uSzXJpJMn

Check token supply,

    $ spl-token supply GjvWk83m1vKQjmQU2dPnLTRvAKjtAKsAmKKNu7Gyk93
    100

Check token balance,

    $ spl-token balance GjvWk83m1vKQjmQU2dPnLTRvAKjtAKsAmKKNu7Gyk93
    100


Accounts,

    $ spl-token accounts
    Token                                         Balance
    ---------------------------------------------------------------
    GjvWk83m1vKQjmQU2dPnLTRvAKjtAKsAmKKNu7Gyk93   100

### Creating NFT
[TODO]
will post after fixing AWS RHEL instance..

### Notes on Ubuntu Build
Requires following,

    sudo apt install pkg-config libssl-dev libudev-dev

Otherwise it complains,

    Package openssl was not found in the pkg-config search path.
    Perhaps you should add the directory containing `openssl.pc' to the PKG_CONFIG_PATH environment variable
    No package 'openssl' found

A successful build looks like this,

    $ tail ~/logs/solana_log.txt
    Compiling zstd v0.5.4+zstd.1.4.7
    Compiling solana-account-decoder v1.7.11
    Compiling solana-transaction-status v1.7.11
    Compiling solana-client v1.7.11
    Compiling solana-cli-output v1.7.11
    Compiling spl-token-cli v2.0.15
        Finished release [optimized] target(s) in 6m 47s
    Installing /home/atiq/.cargo/bin/spl-token
    Installed package `spl-token-cli v2.0.15` (executable `spl-token`)
    warning: be sure to add `/home/atiq/.cargo/bin` to your PATH to be able to run the installed binaries
