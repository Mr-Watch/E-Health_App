from time import gmtime, strftime
import hashlib
import base64
import time
import hmac

client_id = 'e4e3f73a-0ceb-4d37-939e-90ddb1238360'
secret_key = b'8720f366-3769-47e9-8156-187b9c6583b5'


def get_webmd_enc_data():
    gmt_time = time.strftime("%a, %d %b %Y %I:%M:%S %Z", time.gmtime())
    total_params = f"{{{{timestamp:{gmt_time},client_id:{client_id}}}}}"

    total_params = bytearray(total_params, 'ascii')
    enc_data_digest = hmac.new(
        secret_key, total_params, hashlib.sha256).digest()

    enc_data_bytes_64 = base64.b64encode(enc_data_digest)
    enc_data = enc_data_bytes_64.decode('ascii')

    return enc_data, gmt_time
