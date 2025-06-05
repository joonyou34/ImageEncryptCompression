# Image Encryption and Compression

Securing and Compressing Images with Huffman Coding and Linear Feedback Shift Register (LFSR) Technique.

[![showcase.png](https://i.postimg.cc/YSLkhMww/showcase.png)](https://postimg.cc/Jy8SFwnT)

# Table Of Contents
- [About](#about)  
- [Features](#features)
- [Usage](#usage)
- [Benchmarks](#benchmarks)
- [Installation & Running](#installation--running)

## About

The Image Encryption and Compression project combines Linear Feedback Shift Register (LFSR) for secure image encryption with Huffman coding for efficient compression. It processes ```.bmp``` images into compact, encrypted binary files that include all necessary metadata for seamless decompression and decryption.

Users can configure encryption parameters for enhanced security, and the project includes performance benchmarks to evaluate compression ratios and runtime efficiency.

## Features

- 🔒 **Secure Image Encryption**  
  Uses LFSR-based encryption to obfuscate image data, making it unreadable without the correct seed and tap value.

- 📦 **Lossless Image Compression**  
  Builds a Huffman tree for each color channel and extracts corresponding Huffman codes based on the constructed tree based on the histogram of this color channel.

- 🧩 **Customizable Encryption Parameters**  
  Allows users to define seed size, seed value, and tap value to enhance encryption strength.

- 🔁 **Decompression & Decryption**  
  Supports accurate reconstruction of the original image from the compressed encrypted file using stored metadata.

- 🛠️ **Brute-Force Decryption Tool**  
  Includes a helper function to attempt decryption without a known key by analyzing color distribution and comparing results to expected visual characteristics (e.g., average color deviation). Useful for testing robustness or recovering from lost encryption parameters.

- 📊 **Performance Benchmarking**  
  Includes compression ratio statistics and runtime analysis across various image sizes and types.

### Extra Feature: Enhanced Alpha Numeric Key Generation
What is it?
The AlphaLFSR class generates secure alphanumeric keys using a Linear Feedback Shift Register (LFSR) approach. It allows users to input a seed string containing numbers, symbols, and letters, which are then converted into a binary representation for robust encryption. The ```tap value``` parameter enables customized encryption enhancing randomness in the generated keys.

Advantages
* Increased Key Space: Alphanumeric keys have a larger key space than binary (only 0,1) keys, making them more resistant to brute-force attacks.
* User-Friendly: These keys are easier to remember and input by users.
* Reduced Collision Probability: The diversity of characters minimizes the key collisions, enhancing overall security.
* Enhanced Security: The combination of a larger key space and alphanumeric diversity boosts security for encryption processes.

# Usage

[![catEnc.png](https://i.postimg.cc/X7MWXpjN/catEnc.png)](https://postimg.cc/23xMKjLg)

### Forward Operations (Encryption/Compression)
- Open **the desired image** to compress/encrypt
- Provide the `initial seed` and `tap value`
- Click on the desired option
- Results show on right
- Save bin/image if you want

[![encToCat.png](https://i.postimg.cc/hP8kjsZM/encToCat.png)](https://postimg.cc/nCzRS4JQ)

- ### Backward Operations (Decryption/Decompression)
- Open **binary image/file**
- Provide the `initial seed` and `tap value`
- Click on the desired option
- Results show on right
- Save bin/image if you want

# Benchmarks

| Test Name | Compression Ratio (without Encryption) | Compression Ratio (with Encryption) |
|-----------|----------------------------------------|-------------------------------------|
| Easy 1    | 81.8%                                  | 102.7%                              |
| Easy 2    | 25.2%                                  | 25.2%                               |
| Mid 1     | 534%                                   | 626.7%                              |
| Mid 2     | 183.6%                                 | 259.9%                              |
| Large 1   | 73.3%                                  | 94.3%                               |
| Large 2   | 90.8%                                  | 90.8%                               |


## Installation & Running

To run the program locally, follow these steps:

1. **Clone the repository**
2. **Compile and run `Program.cs`**  


